using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Task> Tasks { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }
            if (obj is not Model.Category category) { return false; }
            if (category.Id != Id) { return false; }
            return true;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
