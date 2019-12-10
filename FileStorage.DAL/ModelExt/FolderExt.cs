using FileStorage.DAL.ModelExt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FileStorage.DAL.Model
{
    public partial class Folder : IEntity<Guid>
    {
        [NotMapped]
        Guid IEntity<Guid>.Id
        {
            set { this.Id = value; }
            get { return this.Id; }
        }
    }
}
