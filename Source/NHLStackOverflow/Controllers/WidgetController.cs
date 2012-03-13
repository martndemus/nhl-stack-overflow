using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{
    public class WidgetController : Controller
    {
        private NHLdb db = new NHLdb();

        public ViewResult User()
        {
            return View();
        }

        public ViewResult Account()
        {
            return View();
        }

        public ViewResult Tags()
        {
            var TagsList = from tags in db.Tags
                           orderby tags.Count descending
                           select tags;
            ViewBag.TagList = TagsList;

            return View();
        }
    }
}
