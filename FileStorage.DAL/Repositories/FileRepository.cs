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

        //public override File Find(Guid id)
        //{
        //    var item = dbSet.AsNoTracking().FirstOrDefault(f => f.Id == id);
        //    item.Path = $@"Files\{item.UserId}\{item.FileName}";
        //    if (item != null && item.FolderId != null)
        //    {
        //        var folder = context.Set<Folder>().AsNoTracking().FirstOrDefault(f => f.Id == item.FolderId);
        //        item.Path = folder.Fu
        //    }
        //}
    }
}
