using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FileStorage.DAL.Model
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { set; get; }

        public string LastName { set; get; }

        public virtual ICollection<File> Files { set; get; }

        public virtual ICollection<Folder> Folders { set; get; }
    }
}
