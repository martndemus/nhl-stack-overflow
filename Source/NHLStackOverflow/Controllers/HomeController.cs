using System.Data;
using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{ 
    public class HomeController : Controller
    {
        private NHLdb db = new NHLdb();

        //
        // GET: /

        public ViewResult Index()
        {
            db.Database.Initialize(true);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}