
using Tasks.Domain.Entities;

namespace Tasks.Application.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Task> Tasks { get; }
        IRepository<User> Users { get; }
        void Commit();
    }
}