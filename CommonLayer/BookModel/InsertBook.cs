using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.BookModel
{
    public class InsertBook
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public double ActualPrice { get; set; }
    }
}
