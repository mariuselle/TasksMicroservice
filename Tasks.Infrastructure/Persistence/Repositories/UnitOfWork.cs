
using Tasks.Application.Repositories;
using Tasks.Domain.Entities;

namespace Tasks.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationContext _dbContext;
        private RepositoryBase<Task> _tasks;
        private RepositoryBase<User> _users;

        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Task> Tasks
        {
            get
            {
                return _tasks ??
                    (_tasks = new RepositoryBase<Task>(_dbContext));
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return _users ??
                    (_users = new RepositoryBase<User>(_dbContext));
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
