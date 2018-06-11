using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Repositories
{
    class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(Context context) : base(context) { }
    }
}
