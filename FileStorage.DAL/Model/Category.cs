using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FileStorage.DAL.Model
{
    public partial class Category : IEntity<Guid>
    {
        public Guid Id { set; get; }

        public string CategoryName { set; get; }

        public virtual ICollection<File> Files { set; get; }
    }
}
