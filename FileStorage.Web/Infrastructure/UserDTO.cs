using FileStorage.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Web.Infrastructure
{
    public class UserDTO
    {
        public UserDTO(User user, IEnumerable<string> roles)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Roles = roles;
        }

        public Guid Id { set; get; }
        public string Email { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public IEnumerable<string> Roles { set; get; }
        public bool IsAdmin => Roles.Contains("Admin");
    }
}
