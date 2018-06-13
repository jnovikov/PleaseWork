using System;
using System.ComponentModel.DataAnnotations;

namespace BackendApi.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите название задания")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите дату выполнения задания")]
        public DateTime? WorkTime { get; set; }

        public virtual Deadline Deadline { get; set; }
        public int DeadlineId { get; set; }
        public bool IsDone { get; set; }
    }
}