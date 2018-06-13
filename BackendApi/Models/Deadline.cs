using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

namespace BackendApi.Models
{
    public class Deadline
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите название дедлайна")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите дату, к которой надо выполнить задание")]
        public DateTime? Finish { get; set; }
        
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public ICollection<Task> Tasks {get; set;}

        public int TasksNum => Tasks?.Count ?? 0;
        public int TasksDone => Tasks?.Count(x => x.IsDone) ?? 0;
    }
}