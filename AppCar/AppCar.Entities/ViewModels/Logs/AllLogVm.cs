using System;
using AppCar.Entities.EntityModels;

namespace AppCar.Entities.ViewModels.Logs
{
    public class AllLogVm
    {
        public string UserName { get; set; }

        public OperationLog Operation { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModfiedTable { get; set; }
    }
}
