using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Repositories
{
    class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context) { }
    }
}
