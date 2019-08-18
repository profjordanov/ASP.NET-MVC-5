using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.ComponentModel;

namespace SilverlightODataClient
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private TaskService.TaskDbContext context;

        public MainViewModel(TaskService.TaskDbContext ctx)
        {
            context = ctx;
            Tasks = new DataServiceCollection<TaskService.Task>();
            Users = new DataServiceCollection<TaskService.User>();
            Priorities = new DataServiceCollection<TaskService.TaskPriority>();
            Statuses = new DataServiceCollection<TaskService.TaskStatus>();
        }
        
        public void LoadAsync()
        {
            Users.LoadAsync(context.Users);
            Priorities.LoadAsync(context.Priorities);
            Statuses.LoadAsync(context.Statuses);
            Tasks.LoadAsync(context.Tasks.Expand("Status,Priority,AssignedTo").IncludeTotalCount());
        }

        public DataServiceCollection<TaskService.TaskPriority> Priorities { get; set; }
        public DataServiceCollection<TaskService.TaskStatus> Statuses { get; set; }
        public DataServiceCollection<TaskService.Task> Tasks { get; set; }
        public DataServiceCollection<TaskService.User> Users { get; set; }


        private string status;

        public string StatusMessage 
        {
            get { return status; }
            set
            {
                if (string.Compare(status, value, StringComparison.InvariantCultureIgnoreCase) != 0)
                {
                    status = value;
                    if (PropertyChanged != null)
                        PropertyChanged(null, new PropertyChangedEventArgs("StatusMessage"));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
