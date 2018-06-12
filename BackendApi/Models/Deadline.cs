using System;
using System.Collections.Generic;

namespace BackendApi.Models
{
    public class Deadline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Finish { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Task> Tasks {get; set;}
    }
}