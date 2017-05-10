using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCar.Entities.EntityModel
{
   public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
