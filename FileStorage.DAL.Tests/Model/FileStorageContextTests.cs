using NUnit.Framework;
using FileStorage.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FileStorage.DAL.Repositories.Infrastructure;
using FileStorage.DAL.Repositories;

namespace FileStorage.DAL.Tests.Model
{
    [TestFixture]
    public class FileStorageContextTest
    {
        FileStorageContext context = new FileStorageContext();

        //[OneTimeSetUp]
        //public void GlobalSetup()
        //{

        //}

        [TestCase]
        public void Create_DB()
        {
            IRepository<Folder> repository = new FolderRepository(context);

            var folder = repository.Find(Guid.Parse("0A50C3DE-D48D-479D-9F36-08D77A9316E5"));
            //Arrange

            //Act
            //context.Categories.Add(new Category { CategoryName = "Photos" });

            //context.Admins.Add(new Admin
            //{
            //    Email = "admin email"
            //});
            //context.Users.Add(new User
            //{
            //    Email = "user email"
            //});
            //context.SaveChanges();

            //var admins = context.Admins.ToList();
            //var users = context.Users.ToList();

            var folders = context.Folders.ToList();

            //Assert
            Assert.IsTrue(true);
        }

        //[OneTimeTearDown]
        //public void GlobalTeardown()
        //{
        //    context.Database.EnsureDeleted();
        //}
    }
}