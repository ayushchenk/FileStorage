using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Web.Identity
{
    public class RegisterModel
    {
        [MaxLength(32)]
        public string FirstName { set; get; }

        [MaxLength(32)]
        public string LastName { set; get; }

        [Required]
        [MaxLength(64)]
        public string Email { set; get; }

        [Required]
        public string Password { set; get; }
    }
}
