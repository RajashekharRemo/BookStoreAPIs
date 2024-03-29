using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.OrderAddress
{
    public class OrderToDB
    {
        public string? UName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public long Phone { get; set; }
        public string? State { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

    }
}
