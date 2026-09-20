using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class SalesByCustomer
    {

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = "";
        public int OrdersCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
