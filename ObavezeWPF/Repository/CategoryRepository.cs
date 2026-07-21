using Microsoft.Data.SqlClient;
using ObavezeWPF.CSVConverters;
using ObavezeWPF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetAll()
        {
            var categories = new List<Category>();

            /* if (!IsInitialized())
             {
                 return categories;
             }

             using (var sr = new StreamReader("categories.csv"))
             {
                 var line = string.Empty;

                 while ((line = sr.ReadLine()) != null)
                 {
                     var category = CategoryCSVConverter.FromCSV(line);
                     categories.Add(category);
                 }
             }*/

            string connectionString =@"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select*from Categories";
            var reader =command.ExecuteReader();
            while (reader.Read())
            {
                Model.Category category = new Model.Category();
                category.Id =Convert.ToInt32(reader["CategoryID"]);
                category.Name = reader["CategoryName"].ToString();
                category.IsActive = Convert.ToBoolean(reader["IsCategoryActive"]);
                categories.Add(category);

            }
            conn.Close();
            


            return categories;
        }

        public Category? Get(int id)
        {
            /*var categories = GetAll();
            var wanted = categories.Find((t) => t.Id == id);
            return wanted;*/
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select*from categories where CategoryID=@Id";
            command.Parameters.Add(new SqlParameter("@Id", id));
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                Model.Category category = new Model.Category();
                category.Id = Convert.ToInt32(reader["CategoryID"]);
                category.Name = reader["CategoryName"].ToString();
                category.IsActive = Convert.ToBoolean(reader["IsCategoryActive"]);
                return category;
            }
            else
            {
                return null;
            }
        }

        public void Save(Category category)
        {
            /*var categories = GetAll();

            var isNew = category.Id == 0;
            if (isNew)
            {
                if(categories.Count > 0)
                {
                    var maxId = categories.Max(t => t.Id);
                    category.Id = maxId + 1;
                }
                else
                {
                    category.Id = 1;
                }
                
            }
            else
            {
                var currentIndex = categories.FindIndex((ec) => ec.Id == category.Id);

                if (currentIndex != -1)
                {
                    categories.RemoveAt(currentIndex);
                }
            }

            categories.Add(category);

            using (var sw = new StreamWriter("categories.csv"))
            {
                foreach (var acategory in categories)
                {
                    sw.WriteLine(acategory.ToCSV());
                }
            }*/
            string connectionString = @"Data Source=DESKTOP-CF74DNQ\MSSQLSERVER3;Initial Catalog=projekatPlatforme;Integrated Security=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            if (category.Id == 0)
            {
                command.CommandText ="insert into categories(CategoryName,IsCategoryActive) values (@Name,@IsActive)";
                command.Parameters.Add(new SqlParameter("@Name",category.Name));
                command.Parameters.Add(new SqlParameter("@IsActive",category.IsActive));
                command.ExecuteNonQuery();
                
            }
            else
            {
                command.CommandText ="update categories set CategoryName=@Name , IsCategoryActive=@IsActive where CategoryID=@Id ";
                command.Parameters.Add(new SqlParameter("@Id",category.Id));
                command.Parameters.Add(new SqlParameter("@Name", category.Name));
                command.Parameters.Add(new SqlParameter("@IsActive", category.IsActive));
                command.ExecuteNonQuery();

            }
            conn.Close();
        }

        public bool IsInitialized()
        {
            /*if (!File.Exists("categories.csv"))
            {
                return false;
            }
            return true;*/
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
