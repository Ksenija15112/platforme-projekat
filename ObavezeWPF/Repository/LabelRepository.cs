using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ObavezeWPF.Repository
{
    public class LabelRepository:ILabelRepository
    {
        public  void Dodaj(Model.Label label)
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "insert into labels(Name,Description,Color) values (@Name,@Description,@Color)";
            command.Parameters.Add(new SqlParameter("@Name",label.Name));
            command.Parameters.Add(new SqlParameter("@Description", label.Description));
            command.Parameters.Add(new SqlParameter("@Color", label.Color));
            command.ExecuteNonQuery();
            conn.Close();

        }

        public List<Model.Label> Iscitaj()
        {
            List<Model.Label> lista = new List<Model.Label>();
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select*from labels";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Model.Label label = new Model.Label();
                label.Id = Convert.ToInt32(reader["Id"]);
                label.Name = reader["Name"].ToString();
                label.Description = reader["Description"].ToString();
                label.Color = reader["Color"].ToString();
                lista.Add(label);
            }
            conn.Close();
            return lista;
        }

        public void Izmeni(Model.Label label)
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "update labels set Name=@Name,Description=@Description,Color=@Color where Id=@Id";
            command.Parameters.Add(new SqlParameter("@Name",label.Name));
            command.Parameters.Add(new SqlParameter("@Description", label.Description));
            command.Parameters.Add(new SqlParameter("@Color", label.Color));
            command.Parameters.Add(new SqlParameter("@Id", label.Id));
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void Obrisi(int id)
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "delete from labels where Id=@Id";
            command.Parameters.Add(new SqlParameter("@Id", id));
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
