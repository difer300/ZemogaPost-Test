using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Http.Results;
using Zemoga.Models;
using Zemoga.Service.Controllers;
using Zemoga.Service.Data.Data;

namespace Zemoga.Service.Test
{
    [TestClass]
    public class PostControllerTest : BaseTest<User>
    {
        private readonly PostController _postController;
        private readonly Mock<ICoreDataContext> _db;
        private readonly IFixture _fixture;

        public PostControllerTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _db = new Mock<ICoreDataContext>();
            _postController = new PostController(_db.Object);
        }

        [TestMethod]
        public void GetPost_ValidPosts_ReturnAllPosts()
        {
            //arrange
            var posts = _fixture.Create<List<Post>>();
            var mockSet = CrateDbSet<Post>(posts);

            _db.Setup(m => m.Posts).Returns(() => mockSet.Object);

            //act
            var result = _postController.Get((int)PostStatus.New);

            //assert
            Assert.IsNotNull(((OkNegotiatedContentResult<List<Post>>)result).Content.Count);
        }

        [TestMethod]
        public void GetPostById_ValidPosts_ReturnPost()
        {
            //arrange
            var posts = _fixture.Create<List<Post>>();
            var mockSet = CrateDbSet<Post>(posts);

            _db.Setup(m => m.Posts).Returns(() => mockSet.Object);

            //act
            var result = _postController.GetById(posts[0].Id);

            //assert
            Assert.AreEqual(((OkNegotiatedContentResult<Post>)result).Content.Title, posts[0].Title);
        }

        [TestMethod]
        public void CreatePost_ValidPost_PostCreated()
        {
            //arrange
            var posts = _fixture.Create<List<Post>>();
            var newPost = _fixture.Create<Post>();
            var mockSetPost = CrateDbSet<Post>(posts);
            _db.Setup(m => m.Posts).Returns(() => mockSetPost.Object);

            //act
            var result = _postController.Create(newPost);

            //assert
            Assert.IsTrue(((OkNegotiatedContentResult<Post>)result).Content.Id != 0);
        }

        [TestMethod]
        public void DeletePostById_ValidPosts_DeletedPost()
        {
            //arrange
            var posts = _fixture.Create<List<Post>>();
            var postsStatusChange = _fixture.Create<List<PostStatusChange>>();
            var mockSet = CrateDbSet<Post>(posts);
            var mockSetStatusChange = CrateDbSet<PostStatusChange>(postsStatusChange);

            _db.Setup(m => m.PostStatusChanges).Returns(() => mockSetStatusChange.Object);
            _db.Setup(m => m.Posts).Returns(() => mockSet.Object);

            //act
            var result = _postController.Delete(posts[0].Id);

            //assert
            Assert.IsTrue(((OkNegotiatedContentResult<BoolResponse>)result).Content.Value);
        }

    }
}
