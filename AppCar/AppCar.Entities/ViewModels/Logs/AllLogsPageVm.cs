using System.Collections.Generic;

namespace AppCar.Entities.ViewModels.Logs
{
    public class AllLogsPageVm
    {
        public int CurrentPage { get; set; }

        public int TotalNumberOfPages { get; set; }

        public IEnumerable<AllLogVm> Logs { get; set; }

        public string WantedUserName { get; set; }
    }
}
