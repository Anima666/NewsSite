using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using NewsSite.WebUi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsSite.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Posts()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IPostRepository> mock = new Mock<IPostRepository>();
            mock.Setup(m => m.Posts).Returns(new List<Post>
            {
                new Post { PostId = 1, Title = "News1"},
                new Post { PostId = 2, Title = "News2"},
                new Post { PostId = 3, Title = "News3"},
                new Post { PostId = 4, Title = "News4"},
                new Post { PostId = 5, Title = "News5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Post> result = ((IEnumerable<Post>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("News1", result[0].Title);
            Assert.AreEqual("News2", result[1].Title);
            Assert.AreEqual("News3", result[2].Title);
        }
        [TestMethod]
        public void Can_Edit_Post()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IPostRepository> mock = new Mock<IPostRepository>();
            mock.Setup(m => m.Posts).Returns(new List<Post>
    {
        new Post { PostId = 1, Title = "News1"},
        new Post { PostId = 2, Title = "News2"},
        new Post { PostId = 3, Title = "News3"},
        new Post { PostId = 4, Title = "News4"},
        new Post { PostId = 5, Title = "News5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Post Post1 = controller.Edit(1).ViewData.Model as Post;
            Post Post2 = controller.Edit(2).ViewData.Model as Post;
            Post Post3 = controller.Edit(3).ViewData.Model as Post;

            // Assert
            Assert.AreEqual(1, Post1.PostId);
            Assert.AreEqual(2, Post2.PostId);
            Assert.AreEqual(3, Post3.PostId);
        }
        [TestMethod]
        public void Cannot_Edit_Nonexistent_Post()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IPostRepository> mock = new Mock<IPostRepository>();
            mock.Setup(m => m.Posts).Returns(new List<Post>
    {
        new Post { PostId = 1, Title = "News1"},
        new Post { PostId = 2, Title = "News2"},
        new Post { PostId = 3, Title = "News3"},
        new Post { PostId = 4, Title = "News4"},
        new Post { PostId = 5, Title = "News5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Post result = controller.Edit(6).ViewData.Model as Post;

            // Assert
        }
        [TestMethod]
        public void Can_Delete_Valid_Posts()
        {
            // Организация - создание объекта Post
            Post Post = new Post { PostId = 2, Title = "News2" };

            // Организация - создание имитированного хранилища данных
            Mock<IPostRepository> mock = new Mock<IPostRepository>();
            mock.Setup(m => m.Posts).Returns(new List<Post>
    {
        new Post { PostId = 1, Title = "News1"},
        new Post { PostId = 2, Title = "News2"},
        new Post { PostId = 3, Title = "News3"},
        new Post { PostId = 4, Title = "News4"},
        new Post { PostId = 5, Title = "News5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление игры
            controller.Delete(Post.PostId);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта Post
            mock.Verify(m => m.DeletePost(Post.PostId));
        }
    }
}
