using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.BookModel
{
    public class UserCartDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Auther {  get; set; }
        public double Price { get; set; }
        public string Image {  get; set; }
        public double ActualPrice { get; set; }
        public int Quantity { get; set; }
    }
}
