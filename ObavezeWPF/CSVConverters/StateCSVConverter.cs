using ObavezeWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.CSVConverters
{
    public static class StateCSVConverter
    {
        public static string ToCSV(this State state)
        {
            return $"{state.Id},{state.Name}";
        }


        public static State FromCSV(string csv)
        {
            var state = new State();

            var parts = csv.Split(',');

            state.Id = int.Parse(parts[0]);
            state.Name = parts[1];
            
            return state;
        }
    }
}
