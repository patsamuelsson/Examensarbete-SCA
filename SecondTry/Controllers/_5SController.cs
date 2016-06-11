using SecondTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondTry.Controllers
{

    /* Controller for the 5S tasks */
    public class _5SController : Controller
    {
        private _5SFunctions s5Func = new _5SFunctions();

        public ActionResult Index()
        {
            //Check so the user is logged in
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                loggedInUser user = Session["user"] as loggedInUser;
                if (user.errorText != "") {
                    ViewBag.errorText = user.errorText;
                    user.errorText = "";
                    return View("ErrorView");
                }
                user.lastView = "_5S";

                /* Send in the current user to update 5S tasks
                send that info to the view */

                s5Func.update5S(user);
                if (s5Func._5sDone.Count() != 0) {
                    ViewData["5sDone"] = s5Func._5sDone;
                }
                ViewBag.submitClass = s5Func.submitClass;
                ViewBag.canSubmit = s5Func.canSubmit;
                return View(s5Func._5sTodo);
            }
        }

        [HttpPost, ValidateInput(false)]
        /* Submit the 5s written in either a undone 5S-task or edited 5S-task 
           If the user have choosen to take a picture return a new View, otherwise redirect */
        public ActionResult submit5S() {
            loggedInUser user = Session["user"] as loggedInUser;
            int id = int.Parse(Request["id"]);
            int takePic = int.Parse(Request["takePicVar"]);
            var comment = Request["comment"];
            var ok = Request["ok"];
            s5Func.set5S(user, id, Request["comment"], Request["ok"]);

            if (takePic == 0)
            {
                return RedirectToAction("Index", user.lastView);
            }
            else {
                ViewBag.id = id;
                return View("TakePicture");
            }
        }

        [HttpPost]
        public ActionResult submitPicture() {
            loggedInUser user = Session["user"] as loggedInUser;
            var id = Request["id"];
            var uri = Request["uri"];
            s5Func.saveImage(id, uri, user);
            return RedirectToAction("Index", user.lastView);
        }
    }
}