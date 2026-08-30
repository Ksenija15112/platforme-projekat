using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Model
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }
            if (obj is not Model.State state) { return false; }
            if (state.Id != Id) { return false; }
            return true;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
