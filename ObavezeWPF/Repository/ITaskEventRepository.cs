using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public interface  ITaskEventRepository
    {
        public void Add(Model.TaskEvent taskEvent);

        public List<Model.TaskEvent> GetAll(int taskId);

       

    }
}
