using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ODataServer.Models;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ODataServer.Services
{
    [DataServicesJSONP.JSONPSupportBehavior] //only used for cross domain demos
    public class TaskDataService : DataService<ObjectContext>
    {
        protected override ObjectContext CreateDataSource()
        {

            var ctx = ((IObjectContextAdapter)new TaskDbContext()).ObjectContext;
            ctx.ContextOptions.ProxyCreationEnabled = false;
            return ctx;
        }

        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Tasks", EntitySetRights.All);
            config.SetEntitySetAccessRule("Users", EntitySetRights.All);
            config.SetEntitySetAccessRule("Priorities", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Statuses", EntitySetRights.AllRead);

            config.SetEntitySetPageSize("Tasks", 3);
 
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }

        [ChangeInterceptor("Tasks")]
        public void TaskInterceptor(Task task, UpdateOperations operations)
        {
            if (operations != UpdateOperations.Delete)
            {
                var validationContext = new ValidationContext(task, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(task, validationContext, validationResults, true);
                if (validationResults.Any())
                {
                    throw new DataServiceException(
                        validationResults.Select(
                        (r)=>r.ErrorMessage).Aggregate(
                            (msg1, msg2)=> string.Concat(msg1, Environment.NewLine, msg2)));
                }
            }
        }
    }
}