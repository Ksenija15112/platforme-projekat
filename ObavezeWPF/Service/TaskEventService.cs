using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Service
{
    public  class TaskEventService
    {
        protected Repository.ITaskEventRepository TaskEventRepository { get; set; }

        public TaskEventService(Repository.ITaskEventRepository TaskEventRepository)
        {
            this.TaskEventRepository = TaskEventRepository;
        }

        public void Add(Model.TaskEvent taskEvent)
        {
            TaskEventRepository.Add(taskEvent);
        }

        public List<Model.TaskEvent> GetAll(int taskId)
        {
            return TaskEventRepository.GetAll(taskId);
        }
    }
}
