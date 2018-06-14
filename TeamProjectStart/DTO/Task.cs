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
        public DateTime WorkTime { get; set; }
        public bool IsDone { get; set; }
    }
}
