using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Application.Services;

namespace Tasks.Application.AsyncServices
{
    public static class HangfireService
    {
        public static void RunJobs()
        {
            RecurringJob.AddOrUpdate<TaskService>(
                "Add Tasks",
                service => service.AssignTasksToUsers(),
                Cron.Daily
            );
        }
    }
}
