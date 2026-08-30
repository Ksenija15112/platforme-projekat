using ObavezeWPF.Model;
using ObavezeWPF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Service
{
    public class StateService
    {
        protected IStateRepository StateRepository { get; set; }
        public StateService(IStateRepository stateRepository)
        {
            StateRepository = stateRepository;
        }

        public List<State> GetAll() {  return StateRepository.GetAll(); }
        public State? Get(int id) { return StateRepository.Get(id); }
        public void Save(State state) { StateRepository.Save(state); }

        public void Delete(int id) { StateRepository.Delete(id); }
    }
}
