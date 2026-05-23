using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Model
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }


        public Label(int Id, string Name, string Description, string Color)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Color = Color;
        }
    }
}
