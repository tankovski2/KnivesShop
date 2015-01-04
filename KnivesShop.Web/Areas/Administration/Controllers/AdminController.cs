using System.Web.Mvc;

namespace KnivesShop.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Administration/Admin/
        public ActionResult Index()
        {
            return View();
        }
	}
}