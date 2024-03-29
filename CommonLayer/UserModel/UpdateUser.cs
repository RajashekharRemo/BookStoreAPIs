using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.UserModel
{
    public class UpdateUser
    {
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters")]
        [RegularExpression(@"[A-Za-z0-9!#$%&*'_\-]+@[A-Za-z0-9_]+\.[a-z]{2,3}$", ErrorMessage = "Invalid Email")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        public long Phone { get; set; }
    }
}
