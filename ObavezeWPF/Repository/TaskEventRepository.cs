using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public class TaskEventRepository:ITaskEventRepository
    {
        //upisuje novi dogadjaj u istoriju
        public void Add(Model.TaskEvent taskEvent)
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "insert into TaskEvent(TaskID,EventType,Description,EventDate,UserId) values (@TaskID,@EventType,@Description,@EventDate,@UserId) ";
            SqlParameter sqlParameter = new SqlParameter("@TaskID",taskEvent.TaskId);
            command.Parameters.Add(sqlParameter);
            SqlParameter sqlParameter2 = new SqlParameter("@EventType", taskEvent.EventType);
            command.Parameters.Add(sqlParameter2);
            SqlParameter sqlParameter3 = new SqlParameter("@Description", taskEvent.Description);
            command.Parameters.Add(sqlParameter3);
            SqlParameter sqlParameter4 = new SqlParameter("@EventDate", taskEvent.EventDate);
            command.Parameters.Add(sqlParameter4);
            SqlParameter sqlParameter5 = new SqlParameter("@UserId", taskEvent.UserId);
            command.Parameters.Add(sqlParameter5);
            command.ExecuteNonQuery();
            conn.Close();

        }
        
        //cita istoriju odredjenog taska
        public List<Model.TaskEvent> GetAll(int taskId)
        {
            List<Model.TaskEvent> lista = new List<Model.TaskEvent>();
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select*from TaskEvent where TaskID=@Id";
            command.Parameters.Add(new SqlParameter("@Id", taskId));
            var reader = command.ExecuteReader();
            while(reader.Read())
            {
                Model.TaskEvent taskEvent = new Model.TaskEvent();
                taskEvent.Id = Convert.ToInt32(reader["Id"]);
                taskEvent.TaskId = Convert.ToInt32(reader["TaskId"]);
                taskEvent.EventType = Convert.ToString(reader["EventType"]);
                taskEvent.Description = Convert.ToString(reader["Description"]);
                taskEvent.EventDate = Convert.ToDateTime(reader["EventDate"]);
                taskEvent.UserId = Convert.ToInt32(reader["UserId"]);
                lista.Add(taskEvent);
                

            }
            reader.Close();
            conn.Close();
            return lista;
        }
    }
}
