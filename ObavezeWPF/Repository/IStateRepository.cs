using ObavezeWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Repository
{
    public interface IStateRepository
    {
       // bool IsInitialized();
        List<State> GetAll();
        State? Get(int id);
        void Save(State state);
        void Delete(int id);
    }
}
