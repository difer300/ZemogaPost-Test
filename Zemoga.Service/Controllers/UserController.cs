using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Zemoga.Models;
using Zemoga.Service.Data.Data;

namespace Zemoga.Service.Controllers
{
    public class UserController : ApiController
    {

        private readonly ICoreDataContext _db;

        public UserController(ICoreDataContext dbContext)
        {
            _db = dbContext;
        }

        public UserController()
        {
            _db = new CoreDataContext();
        }

        [HttpGet]
        [Route("api/User")]
        [ResponseType(typeof(ICollection<User>))]
        public IHttpActionResult Get()
        {
            var users = new List<User>();
            try
            {
                users = _db.Users.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("api/User/{id}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult Get(long id)
        {
            var user = new User();
            try
            {
                user = _db.Users
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(user);
        }

    }
}
