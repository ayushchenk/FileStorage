using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using FileStorage.BLL.Model;
using FileStorage.BLL.Service.Infrastructure;
using FileStorage.DAL.Model;
using FileStorage.DAL.Repositories;
using FileStorage.DAL.Repositories.Infrastructure;
using FileStorage.DAL.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileStorage.BLL.Service
{
    public class FileService : IEntityService<FileDTO>
    {
        private readonly ApplicationUnitOfWork unitOfWork;
        private readonly IRepository<File> repository;
        private readonly IRepository<Folder> folderRepository;
        private readonly IMapper mapper;

        public FileService(IMapper mapper, ApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.FileRepository;
            this.folderRepository = unitOfWork.FolderRepository;
            this.mapper = mapper;
        }

        public Guid Add(FileDTO item)
        {
            return repository.Add(mapper.Map<File>(item));
        }

        public Task<Guid> AddAsync(FileDTO item)
        {
            return Task.Run(() => Add(item));
        }

        public FileDTO Find(Guid id)
        {
            var result = mapper.Map<FileDTO>(repository.Find(id));
            if(result != null && result.FolderId != null)
            {
                var folder = folderRepository.Find(result.FolderId.Value);
                result.Path = folder.FullPath + "\\" + result.DiskFileName;
            }
            else if(result.FolderId == null)
            {
                result.Path = $@"Files\{result.UserId}\{result.DiskFileName}";
            }
            return mapper.Map<FileDTO>(result);
        }

        public Task<FileDTO> FindAsync(Guid id)
        {
            return Task.Run(() => Find(id));
        }

        public IEnumerable<FileDTO> FindBy(Expression<Func<FileDTO, bool>> predicate)
        {
            //mapper.Map()
            var expr = mapper.MapExpression<Expression<Func<FileDTO, bool>>, Expression<Func<File, bool>>>(predicate);
            return repository.FindBy(expr).Select(file => mapper.Map<FileDTO>(file));
        }

        public Task<IEnumerable<FileDTO>> FindByAsync(Expression<Func<FileDTO, bool>> predicate)
        {
            return Task.Run(() => FindBy(predicate));
        }

        public IEnumerable<FileDTO> GetAll()
        {
            return repository.GetAll().Select(file => mapper.Map<FileDTO>(file));
        }

        public Task<IEnumerable<FileDTO>> GetAllAsync()
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

        public void Update(FileDTO item)
        {
            repository.Update(mapper.Map<File>(item));
        }

        public Task UpdateAsync(FileDTO item)
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
