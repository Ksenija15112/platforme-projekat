using Microsoft.Data.SqlClient;
using ObavezeWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public class TaskRepository : ITaskRepository
    {
        const string CONNECTION_STRING = "Data Source=10.37.129.2;Initial Catalog=obavezetestdb;User ID=sa;Password=YourStrong@Passw0rd;Trust Server Certificate=True";
        public Model.Task? Get(int id)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                SELECT * FROM Tasks t
                LEFT JOIN Categories c ON c.CategoryID = t.TaskCategoryID
                LEFT JOIN States s ON s.StateID = t.TaskStateID
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
                SELECT * FROM Tasks t
                LEFT JOIN Categories c ON c.CategoryID = t.TaskCategoryID
                LEFT JOIN States s ON s.StateID = t.TaskStateID";


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
                var priorityIndex = reader.GetOrdinal("Priority");
                var dueDateIndex = reader.GetOrdinal("DueDate");
                var categoryIdIndex = reader.GetOrdinal("CategoryID");
                var categoryNameIndex = reader.GetOrdinal("CategoryName");
                var stateIdIndex = reader.GetOrdinal("StateID");
                var stateNameIndex = reader.GetOrdinal("StateName");

                while (reader.Read())
                {
                    var task = new Model.Task()
                    {
                        Id = reader.GetInt32(idIndex),
                        Title = reader.GetString(titleIndex),
                        Description = reader.IsDBNull(descriptionIndex) ? null : reader.GetString(descriptionIndex),
                        Priority = (Priority)Enum.Parse(typeof(Priority), reader.GetString(priorityIndex)),
                        DueDateTime = reader.IsDBNull(dueDateIndex) ? null : reader.GetDateTime(dueDateIndex),
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
                INSERT INTO Tasks (Title, Description, Priority, DueDate, CategoryID, StateID) 
                output inserted.id 
                VALUES (@Title, @Description, @Priority, @DueDate, @CategoryID, @StateID)";



                command.Parameters.Add(new SqlParameter("Title", task.Title));
                command.Parameters.Add(new SqlParameter("Description", task.Description));
                command.Parameters.Add(new SqlParameter("Priority", task.Priority.ToString()));
                command.Parameters.Add(new SqlParameter("DueDate", task.DueDateTime != null ? ((DateTime)task.DueDateTime).ToString("yyyy-MM-dd") : null));
                command.Parameters.Add(new SqlParameter("TaskCategoryID", task.CategoryId));
                command.Parameters.Add(new SqlParameter("TaskStateID", task.StateId));

                var taskId = (int)command.ExecuteScalar();
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
                SET Title = @Title, Description = @Description, Priority = @Priority, DueDate = @DueDate, TaskCategoryID = @TaskCategoryID, TaskStateID = @TaskStateID
                WHERE TaskID = @TaskID
                ";

                command.Parameters.Add(new SqlParameter("TaskID", task.Id));
                command.Parameters.Add(new SqlParameter("Title", task.Title));
                command.Parameters.Add(new SqlParameter("Description", task.Description));
                command.Parameters.Add(new SqlParameter("Priority", task.Priority.ToString()));
                command.Parameters.Add(new SqlParameter("DueDate", task.DueDateTime != null ? ((DateTime)task.DueDateTime).ToString("yyyy-MM-dd") : null));
                command.Parameters.Add(new SqlParameter("TaskCategoryID", task.CategoryId));
                command.Parameters.Add(new SqlParameter("TaskStateID", task.StateId));

                command.ExecuteNonQuery();
            }
        }

        public bool IsInitialized()
        {
            throw new NotImplementedException();
        }
    }
}
