using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FileStorage.BLL.Model
{
    public class FolderDTO
    {
        public Guid Id { set; get; }

        [Required]
        [StringLength(64)]
        public string FolderName { set; get; }

        public Guid? ParentFolderId { set; get; }

        public Guid UserId { set; get; }

        public string ParentPath { set; get; }

        public string FullPath => ParentPath + "\\" + FolderName;
    }
}
