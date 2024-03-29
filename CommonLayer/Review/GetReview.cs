using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Review
{
    public class GetReview
    {
        public string? Review {  get; set; }
        public string? FullName { get; set; }
        public int Stars { get; set; }
        public int BookId { get; set; }
    }
}
