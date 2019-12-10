//using FileStorage.DAL.Model;
//using FileStorage.DAL.Repositories;
//using FileStorage.DAL.Repositories.Infrastructure;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace FileStorage.DAL.Tests.Repositories
//{
//    [TestFixture]
//    public class CategoriesRepositoryTests
//    {
//        IRepository<Category> repository = CategoryRepository.Instance;
//        List<Category> categories = new List<Category>();

//        [SetUp]
//        public void SetUp()
//        {
//            categories.Add(new Category
//            {
//                CategoryName = "Photos"
//            });
//            categories.Add(new Category
//            {
//                CategoryName = "Videos"
//            });
//            categories.Add(new Category
//            {
//                CategoryName = "Files"
//            });
//            categories.Add(new Category
//            {
//                CategoryName = "Books"
//            });

//            FileStorageContext.Instance.Database.EnsureCreated();
//        }

//        [TestCase]
//        public void Add_Test()
//        {
//            //Arrange

//            //Act
//            var added = repository.Add(categories[0]);

//            //Assert
//            Assert.NotNull(added);
//            Assert.AreEqual(categories[0], added);
//        }

//        [TestCase]
//        public void Add_Range_Test()
//        {
//            //Arrange

//            //Act
//            foreach (var item in categories)
//                repository.Add(item);

//            //Assert
//            Assert.AreEqual(categories.Count, repository.GetAll().Count());
//        }

//        [TestCase]
//        public void Find_Existing_Test()
//        {
//            //Arrange

//            //Act
//            var added = repository.Add(categories[0]);
//            var existing = repository.Find(added.Id);

//            //Assert
//            Assert.NotNull(existing);
//            Assert.AreEqual(added.Id, existing.Id);
//        }

//        [TestCase]
//        public void Find_NotExisting_Returns_Null_Test()
//        {
//            //Arrange

//            //Act
//            var found = repository.Find(Guid.NewGuid());

//            //Assert
//            Assert.IsNull(found);
//        }

//        [TestCase]
//        public void Update_Test()
//        {
//            //Arrange
//            string updatedName = "Updated";

//            //Act
//            var added = repository.Add(categories[0]);

//            added.CategoryName = updatedName;
//            repository.Update(added);

//            var updated = repository.Find(added.Id);

//            //Assert
//            Assert.NotNull(updated);
//            Assert.AreEqual(updatedName, updated.CategoryName);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            FileStorageContext.Instance.Database.EnsureDeleted();
//            categories.Clear();
//        }
//    }
//}
