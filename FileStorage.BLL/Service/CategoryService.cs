using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using FileStorage.BLL.Model;
using FileStorage.BLL.Service.Infrastructure;
using FileStorage.DAL.Model;
using FileStorage.DAL.Repositories;
using FileStorage.DAL.Repositories.Infrastructure;
using FileStorage.DAL.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileStorage.BLL.Service
{
    public class CategoryService : IEntityService<CategoryDTO>
    {
        private readonly ApplicationUnitOfWork unitOfWork;
        private readonly IRepository<Category> repository;
        private readonly IMapper mapper;

        public CategoryService(IMapper mapper, ApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.CategoryRepository;
            this.mapper = mapper;
        }

        public Guid Add(CategoryDTO item)
        {
            return repository.Add(mapper.Map<Category>(item));
        }

        public Task<Guid> AddAsync(CategoryDTO item)
        {
            return Task.Run(() => Add(item));
        }

        public CategoryDTO Find(Guid id)
        {
            var result = repository.Find(id);
            return mapper.Map<CategoryDTO>(result);
        }

        public Task<CategoryDTO> FindAsync(Guid id)
        {
            return Task.Run(() => Find(id));
        }

        public IEnumerable<CategoryDTO> FindBy(Expression<Func<CategoryDTO, bool>> predicate)
        {
            //mapper.Map()
            var expr = mapper.MapExpression<Expression<Func<CategoryDTO, bool>>, Expression<Func<Category, bool>>>(predicate);
            return repository.FindBy(expr).Select(category => mapper.Map<CategoryDTO>(category));
        }

        public Task<IEnumerable<CategoryDTO>> FindByAsync(Expression<Func<CategoryDTO, bool>> predicate)
        {
            return Task.Run(() => FindBy(predicate));
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return repository.GetAll().Select(category => mapper.Map<CategoryDTO>(category));
        }

        public Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public bool Remove(Guid id)
        {
            return repository.Remove(id);
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            return Task.Run(() => Remove(id));
        }

        public void Update(CategoryDTO item)
        {
            repository.Update(mapper.Map<Category>(item));
        }

        public Task UpdateAsync(CategoryDTO item)
        {
            return Task.Run(() => Update(item));
        }

        public Task SaveAsync()
        {
            return Task.Run(() => repository.Save());
        }

        public void BeginTransaction()
        {
            unitOfWork.BeginTransaction();
        }

        public void CommitTransaction()
        {
            unitOfWork.Commit();
        }
    }
}
