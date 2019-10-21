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
        [HttpGet]
        [Route("api/User")]
        [ResponseType(typeof(ICollection<User>))]
        public IHttpActionResult Get()
        {
            using (var db = new CoreDataContext())
            {
                var users = new List<User>();
                try
                {
                    users = db.Users.ToList();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(users);
            }
        }

        [HttpGet]
        [Route("api/User/{id}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult Get(long id)
        {
            using (var db = new CoreDataContext())
            {
                var user = new User();
                try
                {
                    user = db.Users
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
}
