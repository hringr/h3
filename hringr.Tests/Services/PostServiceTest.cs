using System;
using System.Linq;
using hringr.Models;
using hringr.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace hringr.Tests.Services
{
    public partial class PostServiceTest
    {
        [TestMethod]
        public void TestGetTwoMostRecentPosts()
        {
            // Arrange
            int postsPerPage = 2;
            int pageNumber = 0;

            // Act
            var result = _service.GetMostRecentPosts(postsPerPage, pageNumber);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void TestGetLastOfMostRecentPosts()
        {
            // Arrange
            int postsPerPage = 2;
            int pageNumber = 1;

            // Act
            var result = _service.GetMostRecentPosts(postsPerPage, pageNumber);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void TestNoMorePostsLeft()
        {
            // Arrange
            int postsPerPage = 2;
            int pageNumber = 2;

            // Act
            var result = _service.GetMostRecentPosts(postsPerPage, pageNumber);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void TestPostsByUser()
        {
            // Arrange
            int postsPerPage = 8;
            int pageNumber = 0;

            // Act
            var result = _service.GetMostRecentPostsForUser(_user1, postsPerPage, pageNumber);

            // Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
