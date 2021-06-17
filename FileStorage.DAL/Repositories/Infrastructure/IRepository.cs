using FileStorage.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL.Repositories.Infrastructure
{
    public interface IRepository<T> where T : class, IEntity<Guid> , new()
    {
        Guid Add(T item);
        void Update(T item);
        T Find(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        bool Remove(Guid id);
        void Save();
    }
}
