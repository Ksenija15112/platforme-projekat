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
    public class StateRepository : IStateRepository
    {
        public List<State> GetAll()
        {
            var states = new List<State>();

            if (!IsInitialized())
            {
                return states;
            }

            using (var sr = new StreamReader("states.csv"))
            {
                var line = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    var state = StateCSVConverter.FromCSV(line);
                    states.Add(state);
                }
            }

            return states;
        }

        public State? Get(int id)
        {
            var states = GetAll();
            var wanted = states.Find((t) => t.Id == id);
            return wanted;
        }

        public void Save(State state)
        {
            var states = GetAll();

            var isNew = state.Id == 0;
            if (isNew)
            {
                if (states.Count > 0)
                {
                    var maxId = states.Max(t => t.Id);
                    state.Id = maxId + 1;
                }
                else
                {
                    state.Id = 1;
                }
                
            }
            else
            {
                var currentIndex = states.FindIndex((es) => es.Id == state.Id);

                if (currentIndex != -1)
                {
                    states.RemoveAt(currentIndex);
                }
            }

            states.Add(state);

            using (var sw = new StreamWriter("states.csv"))
            {
                foreach (var astate in states)
                {
                    sw.WriteLine(astate.ToCSV());
                }
            }
        }

        public bool IsInitialized()
        {
            if (!File.Exists("states.csv"))
            {
                return false;
            }
            return true;
        }
    }
}
