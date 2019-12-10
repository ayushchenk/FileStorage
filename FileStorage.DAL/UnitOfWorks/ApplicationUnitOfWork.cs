using FileStorage.DAL.Model;
using FileStorage.DAL.Repositories;
using FileStorage.DAL.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.UnitOfWorks
{
    public class ApplicationUnitOfWork
    {
        private readonly DbContext context;
        private IRepository<Category> categoryRepository;
        private IRepository<Folder> folderRepository;
        private IRepository<File> fileRepository;
        private IDbContextTransaction transaction;

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(context);
                return categoryRepository;
            }
        }

        public IRepository<Folder> FolderRepository
        {
            get
            {
                if (folderRepository == null)
                    folderRepository = new FolderRepository(context);
                return folderRepository;
            }
        }
        public IRepository<File> FileRepository
        {
            get
            {
                if (fileRepository == null)
                    fileRepository = new FileRepository(context);
                return fileRepository;
            }
        }

        public ApplicationUnitOfWork(FileStorageContext context)
        {
            this.context = context;
        }

        public void BeginTransaction()
        {
            transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                transaction?.Commit();
            }
            catch (Exception exc)
            {
                transaction?.Rollback();
                throw exc;
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
