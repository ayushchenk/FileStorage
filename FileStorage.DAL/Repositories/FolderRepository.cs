using FileStorage.DAL.Model;
using FileStorage.DAL.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FileStorage.DAL.Repositories
{
    public class FolderRepository : GenericRepository<Folder>
    {
        public FolderRepository(DbContext context) : base(context)
        {
        }

        public override Folder Find(Guid id)
        {
            var item = dbSet.AsNoTracking().FirstOrDefault(f => f.Id == id);

            if(item != null)
            {
                var folders = dbSet.AsNoTracking().Where(f => f.UserId == item.UserId);
                item.ParentPath = $"Files\\{item.UserId}";

                string path = string.Empty;
                var parent = folders.FirstOrDefault(f => f.Id == (item.ParentFolderId ?? Guid.Empty));
                while(parent != null)
                {
                    path = $"{parent.FolderName}\\{path}";
                    parent = folders.FirstOrDefault(f => f.Id == (parent.ParentFolderId ?? Guid.Empty));
                }

                item.ParentPath += $"\\{path}";
            }

            return item;
        }
    }
}
