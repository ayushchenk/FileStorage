using FileStorage.DAL.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FileStorage.BLL.Model
{
    public class FileDTO
    {
        public Guid Id { set; get; }

        public string Path { set; get; }

        [Required]
        [MaxLength(256)]
        public string FileName { set; get; }

        public FileAccessibility FileAccessibility { set; get; }

        [MaxLength(32)]
        public string ShortLink { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string Email { set; get; }

        public string FolderName { set; get; }

        public string CategoryName { set; get; }

        public Guid UserId { set; get; }

        public Guid CategoryId { set; get; }

        public Guid? FolderId { set; get; }

        public IFormFile File { set; get; }

        public string DiskFileName => $"{Id}.{FileName}";
    }
}
