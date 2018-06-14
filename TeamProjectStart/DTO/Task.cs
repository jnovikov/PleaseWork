using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectStart.DTO
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? WorkTime { get; set; }
        public bool IsDone { get; set; }
        public int DeadlineId { get; set; }

        public static List<Task> ListFromJson(string json)
        {
            try
            {
                var tasks = JsonConvert.DeserializeObject<List<Task>>(json);
                return tasks;
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                return null;
            }
        }
    }
}
