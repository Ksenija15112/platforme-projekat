using Microsoft.Data.SqlClient;
using ObavezeWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Dodaj(User user)
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "insert into users(Name,Email,Password,ImagePath) values (@Name,@Email,@Password,@ImagePath)";
            command.Parameters.Add(new SqlParameter("@Name", user.Name));
            command.Parameters.Add(new SqlParameter("@Email", user.Email));
            command.Parameters.Add(new SqlParameter("@Password", user.Password));
           
            if (user.ImagePath != null)
            {
                command.Parameters.Add(new SqlParameter("@ImagePath", user.ImagePath));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@ImagePath", DBNull.Value));
            }

            command.ExecuteNonQuery();
            conn.Close();
        }

        public List<User> Iscitaj()
        {
            List<Model.User> lista = new List<Model.User>();
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select*from users";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Model.User user = new Model.User();
                user.Id = Convert.ToInt32(reader["Id"]);
                user.Name = reader["Name"].ToString();
                user.Email = reader["Email"].ToString();
                user.Password = reader["Password"].ToString();
                user.ImagePath = reader["ImagePath"].ToString();
                lista.Add(user);
            }
            conn.Close();
            return lista;
        }

        public Model.User? PronadjiPoEmailu(string email)
        {
            Model.User? user = null;
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select*from Users where email=@email";
            command.Parameters.Add(new SqlParameter("@email",email));
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                user = new Model.User();
                user.Id = Convert.ToInt32(reader["Id"]);
                user.Name = Convert.ToString(reader["Name"]);
                user.Email = Convert.ToString(reader["Email"]);
                user.Password = Convert.ToString(reader["Password"]);
            }
            conn.Close();
            return user;
            
        }

        public void Izmeni(User user)
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "update users set Name=@Name,Email=@Email,Password=@Password,ImagePath=@ImagePath where Id=@Id";
            command.Parameters.Add(new SqlParameter("@Name", user.Name));
            command.Parameters.Add(new SqlParameter("@Email", user.Email));
            command.Parameters.Add(new SqlParameter("@Password", user.Password));
            command.Parameters.Add(new SqlParameter("@Id", user.Id));
            if (user.ImagePath != null)
            {
                command.Parameters.Add(new SqlParameter("@ImagePath", user.ImagePath));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@ImagePath", DBNull.Value));
            }
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void Obrisi(int id)
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "delete from users where Id=@Id";
            command.Parameters.Add(new SqlParameter("@Id", id));
            command.ExecuteNonQuery();
            conn.Close();
        }

        
    }
}
