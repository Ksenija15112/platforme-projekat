using ObavezeWPF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Service
{
    public class TaskService
    {
        protected ITaskRepository TaskRepository { get; set; }
        public TaskService(ITaskRepository taskRepository)
        {
            TaskRepository = taskRepository;
        }

        public List<Model.Task> GetAll() {  return TaskRepository.GetAll(); }
        public Model.Task? Get(int id) { return TaskRepository.Get(id); }
        public void Save(Model.Task task) 
        { 
            TaskRepository.Save(task);
            /*TaskLabelRepository taskLabelRepository = new TaskLabelRepository();
            taskLabelRepository.Obrisi(task.Id);
            foreach(var i in task.Labels)
            {

            }*/
        
        }

        public void Obrisi(int Id)
        {
            TaskLabelRepository taskLabelRepository = new TaskLabelRepository();
            taskLabelRepository.Obrisi(Id);

            TaskRepository.Delete(Id);
        }
    }
}
