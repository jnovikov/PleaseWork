using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectStart.DTO
{
    public class Deadline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Finish { get; set; }
        public uint TasksNum { get; set; }
        public uint TasksDone { get; set; }
        [JsonIgnore]
        public double PercentDone
        {
            set => PercentDone = value;
            get => TasksNum != 0 ? (double)TasksDone / TasksNum * 100 : 0;
        }

        public static Deadline FromJson(string json)
        {
            try
            {
                var deadline = JsonConvert.DeserializeObject<Deadline>(json);
                return deadline;
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                return null;
            }
        }

        public static List<Deadline> ListFromJson(string json)
        {
            try
            {
                var deadlines = JsonConvert.DeserializeObject<List<Deadline>>(json);
                return deadlines;
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                return null;
            }
        }
    }
}
