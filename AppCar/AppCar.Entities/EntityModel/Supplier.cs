using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCar.Entities.EntityModel
{
   public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsImporter { get; set; }
        public virtual ICollection<Part> Parts { get; set; }
    }
}
