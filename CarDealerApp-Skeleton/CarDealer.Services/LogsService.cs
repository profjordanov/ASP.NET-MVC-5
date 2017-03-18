using System.Collections.Generic;
using System.Linq;
using CarDealer.Models;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class LogsService : Service
    {
        public AllLogsPageVm GetAllLogsPageVm(string username, int? page)
        {
            var currentPage = 1;
            if (page != null)
            {
                currentPage = page.Value;
            }

            IEnumerable<Log> logs;
            if (username != null)
            {
                logs = this.Context.Logs.Where(log => log.User.Username == username);
            }
            else
            {
                logs = this.Context.Logs;
            }

            int allLogPagescount = logs.Count() / 20 + (logs.Count() % 20 == 0 ? 0 : 1);
            int logsToTake = 20;
            if (allLogPagescount == currentPage)
            {
                logsToTake = logs.Count() % 20 == 0 ? 20 : logs.Count() % 20;
            }

            logs = logs.Skip((currentPage - 1) * 20).Take(logsToTake);

            List<AllLogVm> logVms = new List<AllLogVm>();
            foreach (var log in logs)
            {
                logVms.Add(new AllLogVm()
                {
                    Operation = log.Operation,
                    ModifiedTable = log.ModifiedTableName,
                    Username = log.User.Username,
                    ModifiedAt = log.ModifiedAt
                });
            }

            AllLogsPageVm pageVm = new AllLogsPageVm()
            {
                WantedUsername = username,
                CurrentPage = currentPage,
                TotalNumberOfPages = allLogPagescount,
                Logs = logVms
            };

            return pageVm;
        }

        public void DeleteAllLogs()
        {
            this.Context.Logs.RemoveRange(this.Context.Logs);
            this.Context.SaveChanges();
        }
    }
}