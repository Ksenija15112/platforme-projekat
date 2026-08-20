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
    public class CSVTaskRepository : ITaskRepository
    {
        protected CategoryRepository CategoryRepo { get; set; }
        protected StateRepository StateRepo { get; set; }
        public CSVTaskRepository()
        {
            CategoryRepo = new CategoryRepository();
            StateRepo = new StateRepository();
        }



        public List<Model.Task> GetAll()
        {
            var tasks = new List<Model.Task>();

            if (!IsInitialized())
            {
                return tasks;
            }

            using (var sr = new StreamReader("tasks.csv"))
            {
                var line = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    var task = TaskCSVConverter.FromCSV(line);
                    if (task.CategoryId != 0)
                    {
                        task.Category = CategoryRepo.Get(task.CategoryId);

                    }
                    if (task.StateId != 0)
                    {
                        task.State = StateRepo.Get(task.StateId);
                    }
                    
                    tasks.Add(task);
                }
            }

            return tasks;
        }

        public Model.Task? Get(int id)
        {
            var tasks = GetAll();
            var wantedTask = tasks.Find((t) => t.Id == id);
            return wantedTask;
        }

        public void Save(Model.Task task)
        {
            var tasks = GetAll();

            var isNew = task.Id == 0;
            if (isNew)
            {
                if(tasks.Count > 0)
                {
                    var maxId = tasks.Max(t => t.Id);
                    task.Id = maxId + 1;
                }
                else
                {
                    task.Id = 1;
                }
            }
            else
            {
                var currentIndex = tasks.FindIndex((et) => et.Id == task.Id);

                if (currentIndex != -1)
                {
                    tasks.RemoveAt(currentIndex);
                }
            }

            tasks.Add(task);

            using(var sw = new StreamWriter("tasks.csv"))
            {
                foreach (var atask in tasks)
                {
                    sw.WriteLine(atask.ToCSV());
                }
            }
        }

        public bool IsInitialized()
        {
            if (!File.Exists("tasks.csv"))
            {
                return false;
            }
            return true;
        }

        public void Delete(int id)
        {

        }
    }
}
