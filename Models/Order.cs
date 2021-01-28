using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Major_project.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int StaffID { get; set; }
        public Staff Staff { get; set; }
        public int FoodID { get; set; }
        public Food Food { get; set; }
        public int StoreID { get; set; }
        public Store Store { get; set; }
    }
}
