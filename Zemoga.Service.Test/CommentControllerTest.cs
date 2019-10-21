using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http.Results;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Zemoga.Models;
using Zemoga.Service.Controllers;
using Zemoga.Service.Data.Data;

namespace Zemoga.Service.Test
{
    [TestClass]
    public class CommentControllerTest : BaseTest<Comment>
    {
        private readonly CommentController _commentController;
        private readonly Mock<ICoreDataContext> _db;
        private readonly IFixture _fixture;

        public CommentControllerTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _db = new Mock<ICoreDataContext>();
            _commentController = new CommentController(_db.Object);
        }

        [TestMethod]
        public void GetComments_ValidComments_ReturnCommentsByPost()
        {
            //arrange
            var comments = _fixture.Create<List<Comment>>();
            var mockSet = CrateDbSet<Comment>(comments);

            _db.Setup(m => m.Comments).Returns(() => mockSet.Object);

            //act
            var result = _commentController.Get(comments[0].Post.Id);

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateComment_ValidComment_CommentCreated()
        {
            //arrange
            var posts = _fixture.Create<List<Post>>();
            var comments = _fixture.Create<List<Comment>>();
            var newComment = _fixture.Create<Comment>();
            var mockSetPost = CrateDbSet<Post>(posts);
            var mockSetComment = CrateDbSet<Comment>(comments);
            _db.Setup(m => m.Posts).Returns(() => mockSetPost.Object);
            _db.Setup(m => m.Comments).Returns(() => mockSetComment.Object);

            //act
            var result = _commentController.Create(posts[0].Id, newComment);

            //assert
            Assert.IsTrue(((OkNegotiatedContentResult<Comment>)result).Content.Id != 0);
        }

    }
}
