using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObavezeWPF.Model
{
    public class TaskEvent
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public int UserId { get; set; }

    }
}
