using ObavezeWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.CSVConverters
{
    public static class CategoryCSVConverter
    {
        public static string ToCSV(this Category category)
        {
            return $"{category.Id},{category.Name},{category.Description}";
        }


        public static Category FromCSV(string csv)
        {
            var category = new Category();

            var parts = csv.Split(',');

            category.Id = int.Parse(parts[0]);
            category.Name = parts[1];
            category.Description = parts[2];
            
            return category;
        }
    }
}
