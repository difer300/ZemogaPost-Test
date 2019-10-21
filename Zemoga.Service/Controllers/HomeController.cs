using System.Web.Mvc;
using Zemoga.Service.Data.Data;

namespace Zemoga.Service.Controllers
{
    public class HomeController : Controller
    {

         private readonly ICoreDataContext _db;

        public HomeController(ICoreDataContext dbContext)
        {
            _db = dbContext;
        }

        public HomeController()
        {
            _db = new CoreDataContext();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
