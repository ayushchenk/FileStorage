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
    public class FolderService : IEntityService<FolderDTO>
    {
        private readonly ApplicationUnitOfWork unitOfWork;
        private readonly IRepository<Folder> folderRepositoty;
        private readonly IRepository<File> fileRepository;
        private readonly IMapper mapper;

        public FolderService(IMapper mapper, ApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.folderRepositoty = unitOfWork.FolderRepository;
            this.fileRepository = unitOfWork.FileRepository;
            this.mapper = mapper;
        }

        public Guid Add(FolderDTO item)
        {
            return folderRepositoty.Add(mapper.Map<Folder>(item));
        }

        public Task<Guid> AddAsync(FolderDTO item)
        {
            return Task.Run(() => Add(item));
        }

        public FolderDTO Find(Guid id)
        {
            var result = folderRepositoty.Find(id);
            return mapper.Map<FolderDTO>(result);
        }

        public Task<FolderDTO> FindAsync(Guid id)
        {
            return Task.Run(() => Find(id));
        }

        public IEnumerable<FolderDTO> FindBy(Expression<Func<FolderDTO, bool>> predicate)
        {
            //mapper.Map()
            var expr = mapper.MapExpression<Expression<Func<FolderDTO, bool>>, Expression<Func<Folder, bool>>>(predicate);
            return folderRepositoty.FindBy(expr).Select(folder => mapper.Map<FolderDTO>(folder));
        }

        public Task<IEnumerable<FolderDTO>> FindByAsync(Expression<Func<FolderDTO, bool>> predicate)
        {
            return Task.Run(() => FindBy(predicate));
        }

        public IEnumerable<FolderDTO> GetAll()
        {
            return folderRepositoty.GetAll().Select(folder => mapper.Map<FolderDTO>(folder));
        }

        public Task<IEnumerable<FolderDTO>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public bool Remove(Guid id)
        {
            var folder = folderRepositoty.Find(id);
            if (folder == null)
                return false;
            unitOfWork.BeginTransaction();
            DeleteRecursive(folder);
            unitOfWork.Commit();
            return true;
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            return Task.Run(() => Remove(id));
        }

        public void Update(FolderDTO item)
        {
            folderRepositoty.Update(mapper.Map<Folder>(item));
        }

        public Task UpdateAsync(FolderDTO item)
        {
            return Task.Run(() => Update(item));
        }

        public Task SaveAsync()
        {
            return Task.Run(() =>
            {
                fileRepository.Save();
                folderRepositoty.Save();
            });
        }

        public void BeginTransaction()
        {
            unitOfWork.BeginTransaction();
        }

        public void CommitTransaction()
        {
            unitOfWork.Commit();
        }

        private void DeleteRecursive(Folder folder)
        {
            var files = fileRepository.FindBy(file => file.FolderId == folder.Id).ToList();
            foreach (var file in files)
                fileRepository.Remove(file.Id);
            folderRepositoty.Remove(folder.Id);

            var subFolders = folderRepositoty.FindBy(f => f.ParentFolderId == folder.Id).ToList();
            foreach (var sub in subFolders)
            {
                DeleteRecursive(sub);
            }
        }
    }
}
