using System.Diagnostics;
using System.Linq;
using Tasks.Application.Repositories;

namespace Tasks.Application.Services
{
    public class TaskService
    {
        private readonly IUnitOfWork unitOfWork;
        public TaskService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AssignTasksToUsers()
        {
            var tasks = unitOfWork.Tasks.Get();

            var unassignedTasks = tasks
                .Where(task => !task.UserId.HasValue);
            if (!unassignedTasks?.Any() ?? true)
            {
                Debug.WriteLine("All tasks are assigned!");

                return;
            }

            var assignedUsers = tasks
                .Where(task => task.UserId.HasValue)
                .GroupBy(task => task.UserId)
                .Select(task => task.First().UserId);

            var unassignedUsers = unitOfWork.Users.Get(user => !assignedUsers.Contains(user.Id));
            if (!unassignedUsers?.Any() ?? true)
            {
                Debug.WriteLine("All users are assigned to a task!");

                return;
            }
            var unassignedUsersEnumerator = unassignedUsers.GetEnumerator();
            foreach (var task in unassignedTasks)
            {
                task.UserId = unassignedUsersEnumerator.Current.Id;
                unitOfWork.Tasks.Update(task);

                if (!unassignedUsersEnumerator.MoveNext())
                    continue;
            }

            unitOfWork.Commit();
        }
    }
}
