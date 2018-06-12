using System;

namespace BackendApi.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime WorkTime { get; set; }
        public Deadline Deadline { get; set; }
        public int DeadlineId { get; set; }
    }
}