using System.Collections.Generic;

namespace AppCar.Entities.EntityModel
{
    public class User
    {
       

        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<Login> Logins { get; set; }
    }
}
