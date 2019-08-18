using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;

namespace WindowsODataClient
{
    public class MainViewModel
    {
        public MainViewModel(TaskService.TaskDbContext ctx)
        {
            Priorities = ctx.Priorities.ToList();
            Statuses = ctx.Statuses.ToList();
            Users = ctx.Users.ToList();

            Tasks = new DataServiceCollection<TaskService.Task>(ctx.Tasks.Expand("Status,Priority,AssignedTo"));
            
        }

        public List<TaskService.TaskPriority> Priorities { get; set; }
        public List<TaskService.TaskStatus> Statuses { get; set; }
        public DataServiceCollection<TaskService.Task> Tasks { get; set; }
        public List<TaskService.User> Users { get; set; }

    }
}
