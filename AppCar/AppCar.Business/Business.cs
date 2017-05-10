using AppCar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCar.Business
{
   public class Business
    {
        protected AppCarDbContext context;
        public Business()
        {
            this.context = new AppCarDbContext();
        }
        protected AppCarDbContext Context => this.context;
    }
}
