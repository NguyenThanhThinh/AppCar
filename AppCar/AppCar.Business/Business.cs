using AppCar.Data;

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
