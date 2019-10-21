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
    public class UserControllerTest : BaseTest<User>
    {
        private readonly UserController _userController;
        private readonly Mock<ICoreDataContext> _db;
        private readonly IFixture _fixture;

        public UserControllerTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _db = new Mock<ICoreDataContext>();
            _userController = new UserController(_db.Object);
        }

        [TestMethod]
        public void GetUsers_ValidUsers_ReturnAllUsers()
        {
            //arrange
            var users = _fixture.Create<List<User>>();
            var mockSet = CrateDbSet<User>(users);

            _db.Setup(m => m.Users).Returns(() => mockSet.Object);

            //act
            var result = _userController.Get();

            //assert
            Assert.IsTrue(((OkNegotiatedContentResult<List<User>>)result).Content.Count == users.Count);
        }

        [TestMethod]
        public void GetUserById_ValidUsers_ReturnUser()
        {
            //arrange
            var users = _fixture.Create<List<User>>();
            var mockSet = CrateDbSet<User>(users);

            _db.Setup(m => m.Users).Returns(() => mockSet.Object);

            //act
            var result = _userController.Get(users[0].Id);

            //assert
            Assert.AreEqual(((OkNegotiatedContentResult<User>)result).Content.Name, users[0].Name);
        }

    }
}
