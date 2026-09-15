using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public  class TaskLabelRepository
    {
        public void Dodaj(int taskId, int labelId)
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "insert into TaskLabels(TaskId,LabelId) values(@TaskId,@LabelId)";
            command.Parameters.Add(new SqlParameter("@TaskId",taskId));
            command.Parameters.Add(new SqlParameter("@LabelId", labelId));
            command.ExecuteNonQuery();
            conn.Close();
        }

        public List<Model.Label> Labels(int taskId)
        {
            List<Model.Label> listaLabela = new List<Model.Label>();
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select*from TaskLabels inner join Labels on TaskLabels.LabelId=Labels.Id where TaskLabels.TaskId=@TaskId";
            command.Parameters.Add(new SqlParameter("@TaskId", taskId));
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Model.Label labela = new Model.Label();
                labela.Id = Convert.ToInt32(reader["Id"]);
                labela.Name = Convert.ToString(reader["Name"]);
                labela.Description = Convert.ToString(reader["Description"]);
                labela.Color = Convert.ToString(reader["Color"]);

                listaLabela.Add(labela);
            }
            
            conn.Close();
            return listaLabela;
            

        }

        public void Obrisi(int taskId)
        {
           
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "delete from TaskLabels where TaskId=@taskId";
            command.Parameters.Add(new SqlParameter("@taskId",taskId));
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void ObrisiLabelu(int labelId)
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "DELETE FROM TaskLabels WHERE LabelId=@labelId";
            command.Parameters.Add(new SqlParameter("@labelId", labelId));

            command.ExecuteNonQuery();
            conn.Close();
        }

    }
}
