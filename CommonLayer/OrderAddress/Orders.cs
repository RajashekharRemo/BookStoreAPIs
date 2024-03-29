using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.OrderAddress
{
    public class Orders
    {
        public int Id { get; set; }
        public string? UName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public long Phone {  get; set; }
        public string? State { get; set; }
        public int Quantity { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Auther { get; set; }
        public string? Image {  get; set; }
        public float Price { get; set; }
        public float ActualPrice { get; set; }
        public string? OrderDate { get; set; }
    }

}
