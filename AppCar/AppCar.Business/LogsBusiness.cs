using System.Collections.Generic;
using System.Linq;
using AppCar.Entities.ViewModels.Logs;
using AppCar.Entities.EntityModels;

namespace AppCar.Business
{
    public class LogsBusiness : Business
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

            int allLogPagesCount = logs.Count() / 20 + (logs.Count() % 20 == 0 ? 0 : 1);
            int logsTotake = 20;
            if (allLogPagesCount == currentPage)
            {
                logsTotake = logs.Count() % 20 == 0 ? 20 : logs.Count() % 20;
            }

            logs = logs.Skip((currentPage - 1) * 20).Take(logsTotake);

            List<AllLogVm> logVms = new List<AllLogVm>();
            foreach (Log log in logs)
            {
                logVms.Add(new AllLogVm()
                {
                    Operation = log.Operation,
                    ModfiedTable = log.ModifiedTableName,
                    UserName = log.User.Username,
                    ModifiedAt = log.ModifiedAt
                });
            }


            AllLogsPageVm pageVm = new AllLogsPageVm()
            {
                WantedUserName = username,
                CurrentPage = currentPage,
                TotalNumberOfPages = allLogPagesCount,
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
