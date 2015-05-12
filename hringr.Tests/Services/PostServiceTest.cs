﻿using System;
using hringr.Models;
using hringr.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace hringr.Tests.Services
{
    [TestClass]
    public class PostServiceTest
    {
        private PostService _service;

        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDatabase();

            var post1 = new Post
            {
                ID = 1,
                date = new DateTime(2015, 04, 10),
                title = "Titill",
                text = "Bla",
                link = "http://mbl.is",
                user = new ApplicationUser(),
                categoryID = 1,
                groupID = 1
            };
            mockDb.Posts.Add(post1);

            var post2 = new Post
            {
                ID = 2,
                date = new DateTime(2015, 05, 10),
                title = "Titill2",
                text = "Bla",
                link = "http://mbl.is",
                user = new ApplicationUser(),
                categoryID = 1,
                groupID = 1
            };
            mockDb.Posts.Add(post2);

            var post3 = new Post
            {
                ID = 3,
                date = new DateTime(2015, 01, 10),
                title = "Titill3",
                text = "Bla",
                link = "http://mbl.is",
                user = new ApplicationUser(),
                categoryID = 1,
                groupID = 1
            };
            mockDb.Posts.Add(post3);

            _service = new PostService(mockDb);
        }

        [TestMethod]
        public void TestGetTwoMostRecentPosts()
        {
            // Arrange
            int postsPerPage = 2;
            int pageNumber = 1;

            // Act
            var result = _service.GetMostRecentPosts(postsPerPage, pageNumber);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestGetLastOfMostRecentPosts()
        {
            // Arrange
            int postsPerPage = 2;
            int pageNumber = 2;

            // Act
            var result = _service.GetMostRecentPosts(postsPerPage, pageNumber);

            // Assert
            Assert.AreEqual(1, result.Count);
        }
    }
}