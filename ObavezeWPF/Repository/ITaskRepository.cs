using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public interface ITaskRepository
    {
        bool IsInitialized();
        List<Model.Task> GetAll();
        Model.Task? Get(int id);
        void Save(Model.Task task);

        void Delete(int id);
    }
}
