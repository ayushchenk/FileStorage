using FileStorage.DAL.Model;
using FileStorage.DAL.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileStorage.DAL.Repositories
{
    public class FileRepository : GenericRepository<File>
    {
        public FileRepository(DbContext context) : base(context)
        {
        }
    }
}
