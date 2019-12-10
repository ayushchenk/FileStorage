using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FileStorage.BLL.Model
{
    public class CategoryDTO
    {
        public Guid Id { set; get; }

        [Required]
        [MaxLength(64)]
        public string CategoryName { set; get; }
    }
}
