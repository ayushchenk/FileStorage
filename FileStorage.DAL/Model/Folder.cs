using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace FileStorage.DAL.Model
{
    public partial class Folder
    {
        public Guid Id { set; get; }

        public string FolderName { set; get; }

        public Guid? ParentFolderId { set; get; }

        public Guid UserId { set; get; }

        public virtual User User { set; get; }

        public Folder ParentFolder { set; get; }

        public virtual ICollection<File> Files { set; get; }

        public string FullPath => ParentPath + "\\" + FolderName;
        
        public string ParentPath { set; get; }
    }
}
