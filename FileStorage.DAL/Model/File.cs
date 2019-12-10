using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FileStorage.DAL.Model
{
    public partial class File
    {
        public Guid Id { set; get; }

        //public string Path { set; get; }

        public string FileName { set; get; }

        public FileAccessibility FileAccessibility { set; get; }

        public string ShortLink { set; get; }

        public Guid UserId { set; get; }

        public Guid? FolderId { set; get; }

        public Guid CategoryId { set; get; }

        public virtual User User { set; get; }

        public virtual Folder Folder { set; get; }

        public virtual Category Category { set; get; }

        //public string Path 
        //{
        //    get
        //    {
        //        if(Folder != null)
        //            return Folder.FullPath + "\\" + FileName;
        //        return $@"Files\{UserId}\{FileName}";
        //    }
        //}

        public string Path { set; get; }
    }

    public enum FileAccessibility
    {
        Private, Protected, Public
    }
}
