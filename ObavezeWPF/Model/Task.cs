using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Model
{
    public class Task : ICloneable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DueDateTime { get; set; }
        public int CategoryId { get; set; }
        //public string CategoryName { get { return category?.Name; } }
        protected Category? category { get; set; }
        public Category? Category {
            get { return category; }
            set
            {
                if (value != null)
                {
                    category = value;
                    CategoryId = value.Id;
                }
                else
                {
                    category = null;
                    CategoryId = 0;
                }
            }
        }
        public int StateId { get; set; }
        protected State? state { get; set; }
        //public string StateName { get { return state?.Name; } }
        public State? State 
        { 
            get { return state; } 
            set
            {
                if (value != null)
                {
                    state = value;
                    StateId = value.Id;
                }
                else
                {
                    state = null;
                    StateId = 0;
                }
            }
        }

        public override string ToString()
        {
            return $"{Id}, {Title}, {Description}, {DueDateTime}, {Category?.Name}, {State?.Name}";
        }

        public override bool Equals(object? obj)
        {
            if(obj == null) { return false; }
            if(obj is not Model.Task task) { return false; }
            if (task.Id != Id) { return false; }
            return true;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public object Clone()
        {
            return new Model.Task()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                DueDateTime = DueDateTime,
                Priority = Priority,
                Category = Category,
                State = State,
            };
        }
    }
}
