using SecondTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondTry.Controllers
{
    public class DDSController : Controller
    {
        private DDSFunctions dds = new DDSFunctions();

        public ActionResult Index()
        {   
            /* Check so that you are logged in 
            and not trying to reach this the wrong way */

            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                loggedInUser user = Session["user"] as loggedInUser;

                //If there is an Errortext return the ErrorView
                if (user.errorText != "")
                {
                    ViewBag.errorText = user.errorText;
                    user.errorText = "";
                    return View("ErrorView");
                }

                user.lastView = "DDS";
                user.updateShift();
                dds.updateDDS(user.Machno, user.selShift.shiftno);
                if (dds.DDSDone.Count() != 0)
                {
                    ViewData["DDSDone"] = dds.DDSDone;
                }
                return View(dds.DDSTodo);
            }
        }

        [HttpPost]
        public ActionResult submitDDS() {
            loggedInUser user = Session["user"] as loggedInUser;
            dds.submitDDS(Request["hiddenId"], Request["hiddenReadValue"], Request["hiddenComment"]);
            return RedirectToAction("index", user.lastView);
        }
    }
}