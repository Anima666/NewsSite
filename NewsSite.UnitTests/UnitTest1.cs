using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using NewsSite.WebUi.Controllers;
using NewsSite.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSite.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //// Организация (arrange)
            //Mock<IPostRepository> mock = new Mock<IPostRepository>();
            //mock.Setup(m => m.Posts).Returns(new List<Post>
            //{
            //    new Post { PostId = 1, Title = "News1"},
            //    new Post { PostId = 2, Title = "News2"},
            //    new Post { PostId = 3, Title = "News3"},
            //    new Post { PostId = 4, Title = "News4"},
            //    new Post { PostId = 5, Title = "News5"}
            //});
            ////PostController controller = new PostController(mock.Object);
            //controller.pageSize = 3;

            //// Действие (act)
            //PostListViewModel result = (PostListViewModel)controller.List(null, 2).Model;

            // Утверждение (assert)
            //List<Post> Post = resul;
            //Assert.IsTrue(Post.Count == 2);
            //Assert.AreEqual(Post[0].Title, "News4");
            //Assert.AreEqual(Post[1].Title, "News5");
        }
    }
}
