using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Core.Entities
{
    public class Order
    {
        public Order() {
            Products = new List<Product>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<Product> Products { get; set; }
    }
}
