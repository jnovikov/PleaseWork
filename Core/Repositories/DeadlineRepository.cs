using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Repositories
{
    class DeadlineRepository : Repository<Deadline>, IDeadlineRepository
    {
        public DeadlineRepository(Context context) : base(context) { }
    }
}
