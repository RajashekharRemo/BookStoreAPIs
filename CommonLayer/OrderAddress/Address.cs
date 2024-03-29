using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.OrderAddress
{
    public class Address
    {
        public int Id { get; set; }
        public string? UName {  get; set; }
        public long UPhone { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string ? UAddress { get; set; }
        public string? AddressType { get; set; }
    }
}
