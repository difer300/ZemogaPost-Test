using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Zemoga.Models;
using Zemoga.Web.Service.Api;

namespace Zemoga.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var userSvc = new UserService();
            ICollection<User> listUsers = await userSvc.GetUsers();
            listUsers.Add(new User() { Id = 0, Name = "Non Authenticated" });
            return View(listUsers);
        }

        [HttpPost]
        public async Task<ActionResult> LoginAsync(FormCollection collection)
        {
            var userSvc = new UserService();
            var idUser = long.Parse(collection["Users"]);
            if (idUser != 0)
            {
                User user = await userSvc.GetUser(idUser);
                Session["CurrentUser"] = user;
            }
            return Redirect("/Post");
        }

    }
}