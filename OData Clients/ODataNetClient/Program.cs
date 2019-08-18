using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;

namespace ODataNetClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskSVC.TaskDbContext ctx =
                new TaskSVC.TaskDbContext(new Uri("http://localhost:44301/odata/"));

            //DataServiceQuery<TaskSVC.Task> query =
            //    ctx.CreateQuery<TaskSVC.Task>("Tasks");

            //RawAPI(ctx);

            //LinqAPI(ctx);

            Paging(ctx);

            

            Console.ReadLine();
        }

       
        private static void Paging(TaskSVC.TaskDbContext ctx)
        {
            DataServiceQuery<TaskSVC.Task> query =
               ctx.Tasks.AddQueryOption("$inlinecount", "allpages");

            QueryOperationResponse<TaskSVC.Task> response =
                query.Execute() as QueryOperationResponse<TaskSVC.Task>;
            
            Console.WriteLine("Total records: {0}\n", response.TotalCount);

            //process the first page
            foreach (var item in response)
            {
                Console.WriteLine(item.Description);
            }

            //get additional pages if there are any
            DataServiceQueryContinuation<TaskSVC.Task> cont =
                response.GetContinuation();

            while (cont != null)
            {
                Console.WriteLine("\n. . .Continuation . . . \n");
                response = ctx.Execute(cont);
                foreach (var item in response)
                {
                    Console.WriteLine(item.Description);
                }
                cont = response.GetContinuation();
            }
        }

        private static void LinqAPI(TaskSVC.TaskDbContext ctx)
        {
            var query = (from t in ctx.Tasks.AddQueryOption("$inlinecount", "allpages")
                         select t).Take(2);

            foreach (var item in query)
            {
                Console.WriteLine(item.Description);
            }
        }

        private static void RawAPI(TaskSVC.TaskDbContext ctx)
        {
            DataServiceQuery<TaskSVC.Task> query =
                ctx.Tasks;
            query = query.AddQueryOption("$top", 2);
            query = query.AddQueryOption("$inlinecount", "allpages");

            QueryOperationResponse<TaskSVC.Task> response =
                query.Execute() as QueryOperationResponse<TaskSVC.Task>;

            
            Console.WriteLine("Total records: {0}", response.TotalCount);
            foreach (var item in response)
            {
                Console.WriteLine(item.Description);
            }
            
        }
    }
}
