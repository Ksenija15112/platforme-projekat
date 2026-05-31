using ObavezeWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.CSVConverters
{
    public static class TaskCSVConverter
    {
        public static string ToCSV(this Model.Task task)
        {
            return $"{task.Id},{task.Title},{task.Description},{task.DueDateTime},{task.Category?.Id},{task.State?.Id}";
        }


        public static Model.Task FromCSV(string csv)
        {
            var task = new Model.Task();

            var parts = csv.Split(',');

            task.Id = int.Parse(parts[0]);
            task.Title = parts[1];
            task.Description = parts[2];
      
            task.DueDateTime = DateTime.Parse(parts[4]);

            if (!string.IsNullOrEmpty(parts[5]))
            {
                task.CategoryId = int.Parse(parts[5]);
            }

            if (!string.IsNullOrEmpty(parts[6]))
            {
                task.StateId = int.Parse(parts[6]);
            }

            return task ;
        }
    }
}
