using Microsoft.Data.SqlClient;
using Microsoft.Win32.SafeHandles;
using ObavezeWPF.Model;
using ObavezeWPF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public class TaskRepository : ITaskRepository
    {
        const string CONNECTION_STRING = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
        public Model.Task? Get(int id)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                SELECT *,l.Name as LabelName,u.Name as UserName FROM Tasks t
                LEFT JOIN Categories c ON c.CategoryID = t.TaskCategoryID
                LEFT JOIN States s ON s.StateID = t.TaskStateID
                LEFT JOIN Labels l on l.Id=t.LabelId
                LEFT JOIN Users u on u.Id=t.UserId
                WHERE TaskID = @TaskID";

                command.Parameters.Add(new SqlParameter("TaskID", id));
                
                var results = ReadTasksFromReader(command);
                if(results.Count > 0)
                {
                    return results[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Model.Task> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                SELECT *,l.Name as LabelName,u.Name as UserName FROM Tasks t
                LEFT JOIN Categories c ON c.CategoryID = t.TaskCategoryID
                LEFT JOIN States s ON s.StateID = t.TaskStateID
                LEFT JOIN Labels l on l.Id = t.LabelId
                LEFT JOIN Users u on u.Id = t.UserId";


                return ReadTasksFromReader(command);
            }
        }

        private static List<Model.Task> ReadTasksFromReader(SqlCommand command)
        {
            var tasks = new List<Model.Task>();

            using (var reader = command.ExecuteReader())
            {
                var idIndex = reader.GetOrdinal("TaskID");
                var titleIndex = reader.GetOrdinal("Title");
                var descriptionIndex = reader.GetOrdinal("Description");
                
                var dueDateIndex = reader.GetOrdinal("DueDate");
                var categoryIdIndex = reader.GetOrdinal("CategoryID");
                var categoryNameIndex = reader.GetOrdinal("CategoryName");
                var stateIdIndex = reader.GetOrdinal("StateID");
                var stateNameIndex = reader.GetOrdinal("StateName");
                var startDateIndex = reader.GetOrdinal("StartDate");
                var storyPointsIndex = reader.GetOrdinal("StoryPoints");
                
                var LabelIdIndex = reader.GetOrdinal("LabelId");
                var LabelNameIndex = reader.GetOrdinal("LabelName");

                
                var UserIdIndex = reader.GetOrdinal("UserId");
                var UserNameIndex = reader.GetOrdinal("UserName");



                while (reader.Read())
                {
                    var task = new Model.Task()
                    {
                        Id = reader.GetInt32(idIndex),
                        Title = reader.GetString(titleIndex),
                        Description = reader.IsDBNull(descriptionIndex) ? null : reader.GetString(descriptionIndex),
                        DueDateTime = reader.IsDBNull(dueDateIndex) ? null : reader.GetDateTime(dueDateIndex),
                        StartDate = reader.GetDateTime(startDateIndex),
                        StoryPoints = reader.GetInt16(storyPointsIndex),
                       
                        CategoryId = reader.GetInt32(categoryIdIndex),
                        Category = new Category()
                        {
                            Id = reader.GetInt32(categoryIdIndex),
                            Name = reader.GetString(categoryNameIndex)
                        },
                        StateId = reader.GetInt32(stateIdIndex),
                        State = new State()
                        {
                            Id = reader.GetInt32(stateIdIndex),
                            Name = reader.GetString(stateNameIndex)
                        }
                    };
                    if (!reader.IsDBNull(LabelIdIndex))
                    {
                        task.Label = new Label()
                        {
                            Id = reader.GetInt32(LabelIdIndex),
                            Name = reader.GetString(LabelNameIndex)

                        };
                    }
                    if (!reader.IsDBNull(UserIdIndex))
                    {
                        task.User = new User()
                        {
                            Id = reader.GetInt32(UserIdIndex),
                            Name = reader.GetString(UserNameIndex)

                        };
                    }

                    tasks.Add(task);
                }

                return tasks;
            }
        }


        public void Save(Model.Task task)
        {
            if(task.Id == -1)
            {
                DoInsert(task);
            }
            else
            {
                DoUpdate(task);
            }
        }

        protected void DoInsert(Model.Task task)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                INSERT INTO Tasks (Title, Description, DueDate, TaskCategoryID, TaskStateID,StartDate,StoryPoints,LabelID,UserID) 
                output inserted.TaskID 
                VALUES (@Title, @Description, @DueDate, @CategoryID, @StateID,@StartDate,@StoryPoints,@LabelID,@UserID)";



                command.Parameters.Add(new SqlParameter("Title", task.Title));
                command.Parameters.Add(new SqlParameter("Description", task.Description));
                command.Parameters.Add(new SqlParameter("DueDate", task.DueDateTime != null ? ((DateTime)task.DueDateTime).ToString("yyyy-MM-dd") : null));
                command.Parameters.Add(new SqlParameter("CategoryID", task.CategoryId));
                command.Parameters.Add(new SqlParameter("StateID", task.StateId));
                command.Parameters.Add(new SqlParameter("StartDate", task.StartDate));
                command.Parameters.Add(new SqlParameter("StoryPoints", task.StoryPoints));
               
                
                if (task.Label != null)
                {
                    command.Parameters.Add(new SqlParameter("LabelID", task.LabelId));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("LabelID", DBNull.Value));
                }
                if (task.User != null)
                {
                    command.Parameters.Add(new SqlParameter("UserID", task.UserId));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("UserID", DBNull.Value));
                }

                task.Id = (int)command.ExecuteScalar();

                //TaskEvent
                // Beleženje događaja kreiranja Task-a
                Model.TaskEvent taskEvent = new Model.TaskEvent();
                 taskEvent.TaskId=task.Id;
                 taskEvent.EventType = "created";
                 taskEvent.Description = "Task was created";
                 taskEvent.EventDate = DateTime.Now;
                 taskEvent.UserId = task.UserId;

                Repository.TaskEventRepository taskEventRepository = new Repository.TaskEventRepository();
                Service.TaskEventService taskEventService = new TaskEventService(taskEventRepository);

                taskEventService.Add(taskEvent);
            }
        }

        protected void DoUpdate(Model.Task task)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                UPDATE Tasks
                SET Title = @Title, Description = @Description, DueDate = @DueDate, TaskCategoryID = @CategoryID, TaskStateID = @StateID,StartDate=@StartDate,StoryPoints=@StoryPoints,LabelID=@LabelID,UserID=@UserID
                WHERE TaskID = @TaskID
                ";

                command.Parameters.Add(new SqlParameter("TaskID", task.Id));
                command.Parameters.Add(new SqlParameter("Title", task.Title));
                command.Parameters.Add(new SqlParameter("Description", task.Description));
                command.Parameters.Add(new SqlParameter("DueDate", task.DueDateTime != null ? ((DateTime)task.DueDateTime).ToString("yyyy-MM-dd") : null));
                command.Parameters.Add(new SqlParameter("CategoryID", task.CategoryId));
                command.Parameters.Add(new SqlParameter("StateID", task.StateId));
                command.Parameters.Add(new SqlParameter("StartDate", task.StartDate));
                command.Parameters.Add(new SqlParameter("StoryPoints", task.StoryPoints));
                if (task.Label != null)
                {
                    command.Parameters.Add(new SqlParameter("LabelID", task.LabelId));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("LabelID", DBNull.Value));
                }
                if (task.User != null)
                {
                    command.Parameters.Add(new SqlParameter("UserID", task.UserId));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("UserID", DBNull.Value));
                }


                command.ExecuteNonQuery();

                //novi zapis istorije

                Model.TaskEvent taskEvent = new Model.TaskEvent();
                taskEvent.TaskId = task.Id;
                taskEvent.EventType = "updated";
                taskEvent.Description = "Task was updated";
                taskEvent.EventDate = DateTime.Now;
                taskEvent.UserId = task.UserId;

                Repository.TaskEventRepository taskEventRepository = new Repository.TaskEventRepository();
                Service.TaskEventService taskEventService = new TaskEventService(taskEventRepository);

                taskEventService.Add(taskEvent);
            }
        }

        public void Delete(int Id)
        {
            Model.Task task = Get(Id);
        

            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "DELETE FROM Tasks where TaskID=@TaskID";
            SqlParameter parameter = new SqlParameter("TaskID",Id);
            command.Parameters.Add(parameter);
            

            Model.TaskEvent taskEvent = new Model.TaskEvent();

            taskEvent.TaskId = task.Id;
            taskEvent.EventType = "deleted";
            taskEvent.Description = "Task was deleted";
            taskEvent.EventDate = DateTime.Now;
            taskEvent.UserId = task.UserId;

            Repository.TaskEventRepository taskEventRepository = new Repository.TaskEventRepository();
            Service.TaskEventService taskEventService = new TaskEventService(taskEventRepository);

            taskEventService.Add(taskEvent);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public bool IsInitialized()
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
