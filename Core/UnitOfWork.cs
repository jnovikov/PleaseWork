using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Repositories;

namespace Core
{
    public class UnitOfWork : IDisposable
    {
        Context _context = new Context();

        public IUserRepository Users { get; }
        public IDeadlineRepository Deadlines { get; }
        public ITaskRepository Tasks { get; }

        public UnitOfWork()
        {
            Users = new UserRepository(_context);
            Deadlines = new DeadlineRepository(_context);
            Tasks = new TaskRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
