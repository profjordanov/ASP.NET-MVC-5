using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Services.Client;

namespace SilverlightODataClient
{
    public partial class MainPage : UserControl
    {
        private string SWT_Token;
        private string FormsCookie;
        
        TaskService.TaskDbContext context;
        MainViewModel model;

        public MainPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            context =
               new TaskService.TaskDbContext(
                   new Uri("https://localhost:44302/odata/"));

            context.UseDefaultCredentials = false;
            context.Credentials =
                new NetworkCredential("bob", "password1");

            model = new MainViewModel(context);
            model.Tasks.LoadCompleted += new EventHandler<LoadCompletedEventArgs>(taskCollection_LoadCompleted);
            model.LoadAsync();

            this.DataContext = model;
        }


        

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {   
            context.BeginSaveChanges(SaveChangesOptions.Batch,
                new AsyncCallback(SaveChanges_Complete), null);         
        }

        

        public void SaveChanges_Complete(IAsyncResult iar)
        {
            string statusText = string.Empty;

            try{
                DataServiceResponse response = 
                     context.EndSaveChanges(iar);

            //if batch
                if (response.IsBatchResponse)
                {
                    string statusCode = response.BatchStatusCode.ToString();
                    statusText = "Batch status: " + statusCode + " Item codes(";
                }
                else
                {
                    statusText = "Item status codes(";
                }

                foreach (var item in response)
                {
                    statusText += "[" + item.StatusCode.ToString() + "]";
                }
                statusText += ")";

                //update status message on UI thread
                Dispatcher.BeginInvoke(() =>
                {
                    model.StatusMessage = statusText;
                });
            }
            catch (DataServiceRequestException dsre)
            {
                model.StatusMessage = dsre.InnerException.Message;
            }
            catch (DataServiceClientException dsce)
            {
                model.StatusMessage = dsce.InnerException.Message;
            }
        }

        private void nextPage_Click(object sender, RoutedEventArgs e)
        {
            if (model.Tasks.Continuation != null)
            {
                model.Tasks.LoadCompleted += new EventHandler<LoadCompletedEventArgs>(taskCollection_LoadCompleted);
                model.Tasks.LoadNextPartialSetAsync();
                //context.BeginExecute<TaskService.Task>(model.Tasks.Continuation, 
                //    new AsyncCallback(ExecuteCompleted), null);  
            }
            else
            {
                nextPage.IsEnabled = false;
            }
        }


        public void taskCollection_LoadCompleted(object sender, LoadCompletedEventArgs e)
        {
            //show any errors or load status
            if (e.Error != null)
            {
                model.StatusMessage = e.Error.Message;
            }
            else
            {
                model.StatusMessage = String.Format("Loaded {0} of {1}", model.Tasks.Count, e.QueryOperationResponse.TotalCount);
            }


            //disable paging if no continuation token
            if (model.Tasks.Continuation == null)
                nextPage.IsEnabled = false;
            //remove event handler
            model.Tasks.LoadCompleted -= taskCollection_LoadCompleted;
        }
    }
}
