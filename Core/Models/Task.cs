﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DtFinish { get; set; }
    }
}
