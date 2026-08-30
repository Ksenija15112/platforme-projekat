using Microsoft.Data.SqlClient;
using ObavezeWPF.CSVConverters;
using ObavezeWPF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ObavezeWPF.Repository
{
    public class StateRepository : IStateRepository
    {
        public List<State> GetAll()
        {
            /*var states = new List<State>();

            if (!IsInitialized())
            {
                return states;
            }

            using (var sr = new StreamReader("states.csv"))
            {
                var line = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    var state = StateCSVConverter.FromCSV(line);
                    states.Add(state);
                }
            }

            return states;*/

            List<Model.State> lista = new List<Model.State>();

            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = "select*from states";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                State state = new State();
                state.Id = Convert.ToInt32(reader["StateID"]);
                state.Name = Convert.ToString(reader["StateName"]);
                state.IsActive = Convert.ToBoolean(reader["IsStateActive"]);
                lista.Add(state);
            }

            reader.Close();
            conn.Close();

            return lista;



        }

        public State? Get(int id)
        {
            /*var states = GetAll();
            var wanted = states.Find((t) => t.Id == id);
            return wanted;*/

            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = "select*from states where StateID=@Id";
            SqlParameter sqlParameter = new SqlParameter("@Id", id);
            sqlCommand.Parameters.Add(sqlParameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {

                State state = new State();
                state.Id = Convert.ToInt32(reader["StateID"]);
                state.Name = Convert.ToString(reader["StateName"]);
                state.IsActive = Convert.ToBoolean(reader["IsStateActive"]);
                reader.Close();
                conn.Close();
                return state;

            }
            reader.Close();
            conn.Close();
            return null;
        }

        public void Save(State state)
        {
            /*var states = GetAll();

            var isNew = state.Id == 0;
            if (isNew)
            {
                if (states.Count > 0)
                {
                    var maxId = states.Max(t => t.Id);
                    state.Id = maxId + 1;
                }
                else
                {
                    state.Id = 1;
                }
                
            }
            else
            {
                var currentIndex = states.FindIndex((es) => es.Id == state.Id);

                if (currentIndex != -1)
                {
                    states.RemoveAt(currentIndex);
                }
            }

            states.Add(state);

            using (var sw = new StreamWriter("states.csv"))
            {
                foreach (var astate in states)
                {
                    sw.WriteLine(astate.ToCSV());
                }
            }
        }

        public bool IsInitialized()
        {
            if (!File.Exists("states.csv"))
            {
                return false;
            }
            return true;
        }*/

            if (state.Id == 0)
            {
                string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandText = "insert into states(StateName,IsStateActive) " +
                    "values (@Name,@IsActive)";
                SqlParameter sqlParameter = new SqlParameter("@Name",state.Name);
                sqlCommand.Parameters.Add(sqlParameter);
                SqlParameter sqlParameter2 = new SqlParameter("@IsActive", state.IsActive);
                sqlCommand.Parameters.Add(sqlParameter2);
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandText = "update states set StateName=@Name, IsStateActive=@IsActive where StateID=@Id";
                SqlParameter sqlParameter1 = new SqlParameter("@Id", state.Id);
                sqlCommand.Parameters.Add(sqlParameter1);
                SqlParameter sqlParameter2 = new SqlParameter("@Name", state.Name);
                sqlCommand.Parameters.Add(sqlParameter2);
                SqlParameter sqlParameter3= new SqlParameter("@IsActive", state.IsActive);
                sqlCommand.Parameters.Add(sqlParameter3);
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Delete(int id)
        {
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = "delete from states where StateId=@Id";
            SqlParameter sqlParameter = new SqlParameter("@Id",id);
            sqlCommand.Parameters.Add(sqlParameter);
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }
    }
}
