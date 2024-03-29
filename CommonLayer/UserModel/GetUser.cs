using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.UserModel
{
    public class GetUser
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public long Phone { get; set; }
        public string? Role { get; set; }
        
    }
}
