using NUnit.Framework;
using FileStorage.BLL.Service;
using System;
using System.Collections.Generic;
using System.Text;
using FileStorage.BLL.Service.Infrastructure;
using FileStorage.BLL.Model;
using FileStorage.DAL.Model;
using System.Linq;
using AutoMapper;

namespace FileStorage.BLL.Service.Tests
{
    [TestFixture()]
    public class CategoryServiceTests
    {
        IEntityService<CategoryDTO> service;
        List<CategoryDTO> categories = new List<CategoryDTO>();

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(expr => expr.AddProfile<MappingProfile>());
            IMapper mapper = config.CreateMapper();
            this.service = new CategoryService(mapper);

            categories.Add(new CategoryDTO
            {
                CategoryName = "Photos"
            });
            categories.Add(new CategoryDTO
            {
                CategoryName = "Videos"
            });
            categories.Add(new CategoryDTO
            {
                CategoryName = "Files"
            });
            categories.Add(new CategoryDTO
            {
                CategoryName = "Books"
            });

            FileStorageContext.Instance.Database.EnsureCreated();
        }

        [TestCase]
        public void Add_Test()
        {
            //Arrange

            //Act
            var added = service.Add(categories[0]);

            //Assert
            Assert.NotNull(added);
            Assert.AreEqual(categories[0].CategoryName, added.CategoryName);
        }

        [TestCase]
        public void Add_Range_Test()
        {
            //Arrange

            //Act
            foreach (var item in categories)
                service.Add(item);

            //Assert
            Assert.AreEqual(categories.Count, service.GetAll().Count());
        }

        [TestCase]
        public void Find_Existing_Test()
        {
            //Arrange

            //Act
            var added = service.Add(categories[0]);
            var existing = service.Find(added.Id);

            //Assert
            Assert.NotNull(existing);
            Assert.AreEqual(added.Id, existing.Id);
        }

        [TestCase]
        public void Find_NotExisting_Returns_Null_Test()
        {
            //Arrange

            //Act
            var found = service.Find(Guid.NewGuid());

            //Assert
            Assert.IsNull(found);
        }

        [TestCase]
        public void Update_Test()
        {
            //Arrange
            string updatedName = "Updated";

            //Act
            var added = service.Add(categories[0]);

            added.CategoryName = updatedName;
            service.Update(added);

            var updated = service.Find(added.Id);

            //Assert
            Assert.NotNull(updated);
            Assert.AreEqual(updatedName, updated.CategoryName);
        }

        [TestCase]
        public void Remove_Test()
        {
            //Arrange

            //Act
            var item = service.Add(categories[0]);
            service.Remove(item);

            //Assert
        }


        [TearDown]
        public void TearDown()
        {
            FileStorageContext.Instance.Database.EnsureDeleted();
            categories.Clear();
        }
    }
}