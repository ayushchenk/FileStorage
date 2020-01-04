using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileStorage.BLL.Service.Infrastructure
{
    public interface IEntityService<TDTO>
    {
        Guid Add(TDTO item);
        Task<Guid> AddAsync(TDTO item);

        void Update(TDTO item);
        Task UpdateAsync(TDTO item);

        TDTO Find(Guid id);
        Task<TDTO> FindAsync(Guid id);

        IEnumerable<TDTO> GetAll();
        Task<IEnumerable<TDTO>> GetAllAsync();

        IEnumerable<TDTO> FindBy(Expression<Func<TDTO, bool>> predicate);
        Task<IEnumerable<TDTO>> FindByAsync(Expression<Func<TDTO, bool>> predicate);

        bool Remove(Guid id);
        Task<bool> RemoveAsync(Guid id);

        void BeginTransaction();
        void CommitTransaction();
        Task SaveAsync();
    }
}
