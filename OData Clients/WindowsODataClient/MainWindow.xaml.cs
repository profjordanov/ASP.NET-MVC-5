//#define BASIC
#define OAUTH
//#define FORMS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Data.Services.Client;
using System.Collections.Specialized;
using System.Web;
using System.IO;
using System.Security;

namespace WindowsODataClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string SWT_Token;
        private string FormsCookie;
        private DataServiceCollection<TaskService.Task> taskCollection;
        TaskService.TaskDbContext context;
        MainViewModel model;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context =
                new TaskService.TaskDbContext(
                    new Uri("http://localhost:44301/odata/"));

            //ApplySecurityToContext(context);
            
            model = new MainViewModel(context);

            this.DataContext = model;
            
        }

 
        private void nextPage_Click(object sender, RoutedEventArgs e)
        {
            //manual paging initiated by the user
            if (model.Tasks.Continuation != null)
            {
                model.Tasks.Load(
                    context.Execute<TaskService.Task>(model.Tasks.Continuation));
               
                if (model.Tasks.Continuation == null)
                    nextPage.IsEnabled = false;
            }
            else
            {
                nextPage.IsEnabled = false;
            }
        }


        private void ApplySecurityToContext(TaskService.TaskDbContext context)
        {
#if OAUTH
            if (string.IsNullOrEmpty(SWT_Token))
                RetrieveSWTToken();

            context.SendingRequest += (s, e)=>{
                e.RequestHeaders.Add(HttpRequestHeader.Authorization, 
                    String.Format("Bearer {0}", SWT_Token));
            };

            
#endif
            
            #region OtherAuth
#if FORMS

            if (string.IsNullOrEmpty(FormsCookie))
                RetrieveFormsCookie();

            context.SendingRequest += (s, e) =>
            {
                e.RequestHeaders.Add(HttpRequestHeader.Cookie, FormsCookie);
            };
#endif

            
#if BASIC
            context.Credentials =
                new NetworkCredential(ClientSecurityConfiguration.FormsUserName, ClientSecurityConfiguration.FormsPwd);

#endif 

           

#endregion OtherAuth

        }

        private void RetrieveFormsCookie()
        {
                string loginUri = string.Format("{0}/{1}/{2}",
                    "https://localhost:44302",
                    "Authentication_JSON_AppService.axd",
                    "Login");
                WebRequest request = HttpWebRequest.Create(loginUri);
                request.Method = "POST";
                request.ContentType = "application/json";

                string authBody = String.Format(
                    "{{ \"userName\": \"{0}\", \"password\": \"{1}\", \"createPersistentCookie\":false}}",
                    ClientSecurityConfiguration.FormsUserName,
                    ClientSecurityConfiguration.FormsPwd);
                request.ContentLength = authBody.Length;

                StreamWriter w = new StreamWriter(request.GetRequestStream());
                w.Write(authBody);
                w.Close();

                WebResponse res = request.GetResponse();
                if (res.Headers["Set-Cookie"] != null)
                {
                    FormsCookie = res.Headers["Set-Cookie"];
                }
                else
                {
                    throw new SecurityException("Invalid username and password");
                }
            }
            

        private void RetrieveSWTToken()
        {
 	        WebClient client = new WebClient();
            client.BaseAddress = string.Format("https://{0}.{1}", ClientSecurityConfiguration.ServiceNamespace, ClientSecurityConfiguration.AcsHostUrl);

            NameValueCollection values = new NameValueCollection();
            values.Add("wrap_name", ClientSecurityConfiguration.OAuthUserName);
            values.Add("wrap_password", ClientSecurityConfiguration.OAuthPwd);
            values.Add("wrap_scope", ClientSecurityConfiguration.RelyingPartyRealm);
            try
            {
                byte[] responseBytes = client.UploadValues("WRAPv0.9/", "POST", values);

                string response = Encoding.UTF8.GetString(responseBytes);


                SWT_Token = HttpUtility.UrlDecode(
                    response
                    .Split('&')
                    .Single(value => value.StartsWith("wrap_access_token=", StringComparison.OrdinalIgnoreCase))
                    .Split('=')[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error securing connection");
            }

        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                DataServiceResponse response = 
                    context.SaveChanges(SaveChangesOptions.ContinueOnError | SaveChangesOptions.ReplaceOnUpdate);

                //if batch
                if (response.IsBatchResponse)
                {
                    string statusCode = response.BatchStatusCode.ToString();
                    Status.Text = "Batch status: " + statusCode + " Item codes(";
                }
                else
                {
                    Status.Text = "Item status codes(";
                }

                foreach (var item in response)
                {
                    Status.Text += "[" + item.StatusCode.ToString() + "]";
                }
                Status.Text += ")";
                
                
            }
            catch (DataServiceRequestException dsre)
            {
                
                MessageBox.Show(dsre.InnerException.Message, "Error saving");
            }
            catch (DataServiceClientException dsce)
            {
                MessageBox.Show(dsce.InnerException.Message, "Error saving");
            }
        }

       
    }
}
