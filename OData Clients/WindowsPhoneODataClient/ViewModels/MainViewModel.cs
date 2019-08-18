using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using SharpCompress.Compressor.Deflate;


namespace WindowsPhoneODataClient
{

    public class MainViewModel
    {
        internal TaskService.TaskDbContext context;


        public MainViewModel(TaskService.TaskDbContext ctx) :this(ctx, false)
        {

        }
        public MainViewModel(TaskService.TaskDbContext ctx, bool enableCompression)
        {
            context = ctx;
            if (enableCompression)
                EnableCompressionOnContext(context);

            Tasks = new DataServiceCollection<TaskService.Task>();
            Users = new DataServiceCollection<TaskService.User>();
            Priorities = new DataServiceCollection<TaskService.TaskPriority>();
            Statuses = new DataServiceCollection<TaskService.TaskStatus>();
        }

        /// <summary>
        /// Constructor used when loading model from saved state
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="tasks"></param>
        /// <param name="users"></param>
        /// <param name="priorities"></param>
        /// <param name="statuses"></param>
        public MainViewModel(TaskService.TaskDbContext ctx,
            DataServiceCollection<TaskService.Task> tasks,
            DataServiceCollection<TaskService.User> users,
            DataServiceCollection<TaskService.TaskPriority> priorities,
            DataServiceCollection<TaskService.TaskStatus> statuses)
        {
            this.context = ctx;
            this.Tasks = tasks;
            this.Users = users;
            this.Priorities = priorities;
            this.Statuses = statuses;
        }


        private void EnableCompressionOnContext(TaskService.TaskDbContext ctx)
        {
            ctx.WritingRequest += (o, rwea) =>
            {
                rwea.Headers["Accept-Encoding"] = "gzip";  
                
                //NOT USED WITH IIS
                //rwea.Headers["Content-Encoding"] = "gzip";
                //rwea.Content = new GZipStream(rwea.Content, SharpCompress.Compressor.CompressionMode.Compress);
            };

            ctx.ReadingResponse += (o, rwea) =>
                {
                    if (rwea.Headers.ContainsKey("Content-Encoding") &&
                        rwea.Headers["Content-Encoding"].Contains("gzip"))
                    {
                        rwea.Content = new GZipStream(rwea.Content, SharpCompress.Compressor.CompressionMode.Decompress);
                    }
                };
        }

        public void LoadAsync()
        {
            Users.LoadAsync(context.Users);
            Priorities.LoadAsync(context.Priorities);
            Statuses.LoadAsync(context.Statuses);
            Tasks.LoadAsync(context.Tasks.Expand("Status,Priority,AssignedTo"));
        }

        public DataServiceCollection<TaskService.TaskPriority> Priorities { get; set; }
        public DataServiceCollection<TaskService.TaskStatus> Statuses { get; set; }
        public DataServiceCollection<TaskService.Task> Tasks { get; set; }
        public DataServiceCollection<TaskService.User> Users { get; set; }

    }
}