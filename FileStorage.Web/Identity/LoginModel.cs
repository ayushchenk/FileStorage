using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Web.Identity
{
    public class LoginModel
    {
        [MaxLength(64)]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
