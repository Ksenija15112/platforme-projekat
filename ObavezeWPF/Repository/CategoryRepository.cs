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

            if (!IsInitialized())
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
            }

            return categories;
        }

        public Category? Get(int id)
        {
            var categories = GetAll();
            var wanted = categories.Find((t) => t.Id == id);
            return wanted;
        }

        public void Save(Category category)
        {
            var categories = GetAll();

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
            }
        }

        public bool IsInitialized()
        {
            if (!File.Exists("categories.csv"))
            {
                return false;
            }
            return true;
        }
    }
}
