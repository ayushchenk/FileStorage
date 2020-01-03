using FileStorage.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL.Repositories.Infrastructure
{
    public class GenericRepository<T> : IRepository<T> where T : class, IEntity<Guid>, new()
    {
        protected readonly DbContext context;
        protected readonly DbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual Guid Add(T item)
        {
            var result = dbSet.Add(item);
            return result.CurrentValues.GetValue<Guid>("Id");
        }

        public virtual T Find(Guid id)
        {
            return dbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.AsNoTracking().AsEnumerable();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).AsNoTracking().AsEnumerable();
        }
        
        public virtual bool Remove(Guid id)
        {
            var item = Find(id);
            if (item == null)
                return false;
            context.Remove(item);
            return true;
        }

        public virtual void Update(T item)
        {
            dbSet.Update(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
