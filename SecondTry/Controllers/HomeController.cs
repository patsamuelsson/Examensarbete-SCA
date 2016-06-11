using System;
using System.Linq;
using System.Web.Mvc;
using SecondTry.Models;

namespace SecondTry.Controllers
{
    /*
        Index Controller, handles the partial views shown on top of the webpage 
        (Userinfo and Menu), it also takes care of logging in.
    */
    public class HomeController : Controller
    {

        dbContext context = new dbContext();
        private _5SFunctions s5Func = new _5SFunctions();
        private DDSFunctions dds = new DDSFunctions();

        /* PartialView on the top of the webpage, displays 
        information about the user. */
        public ActionResult MachinePartialView() {

            //If user is null, you are not logged in, return empty view.
            if (Session["user"] != null)
            {
                //Get logged in user, check what shift is on and getShift(x) x days back.
                loggedInUser user = Session["user"] as loggedInUser;
                ViewBag.selectedShiftNo = user.selShift.shiftno.ToString();
                ViewBag.allShift = user.getShiftBack(3);

                //Is the user locked to a machine
                if (!user.locked) { ViewBag.allMachines = user.allMachines(); }
                //Are the machine a producing machine?
                if (user.prodMachine()) {
                    var MachTarFun = new MachineTargetFunctions(context, user);
                    if (MachTarFun.prodShift == null) {
                        user.errorText = "Could not fetch shift production data!";
                        return new EmptyResult();
                    }
                    ViewBag.tarRes = MachTarFun.calcTargetResult();
                }
                return PartialView(user);
            }

            return new EmptyResult();
        }

        public ActionResult MenuPartialView() {
            //If user is null, you are not logged in, return empty view.
            if (Session["user"] != null)
            {
                loggedInUser user = Session["user"] as loggedInUser;
                ViewBag.lastView = user.lastView;
                if (user.userlevel == "rst")
                {
                    ViewBag.admin = 1;
                }
                else {
                    ViewBag.admin = 0;
                }
                s5Func.update5S(user);
                ViewBag._5sTodo = s5Func.numTodo;

                dds.updateDDS(user.Machno, user.selShift.shiftno);
                ViewBag.DDSTodo = dds.numDDSTodo;

                return PartialView();
            }
            else {
                return new EmptyResult();
            }
        }

        [HttpPost]
        //Called when changing shifts
        public ActionResult changeShift() {

            //Get the selection and set as selected shift
            int selectedShift = int.Parse(Request["selectedShiftNo"]);
            loggedInUser user = Session["user"] as loggedInUser;
            user.selShift = (from d in context.MachineShift where d.shiftno == selectedShift && d.DESCRIPTION == user.maskin select d).First();
            
            //When shiftchange is complete redirect to last view
            return RedirectToAction("Index", user.lastView);
        }

        [HttpPost]
        //Called when the user changes machine
        public ActionResult changeMachine() {
            loggedInUser user = Session["user"] as loggedInUser;
            user.changeMachine(Request["selectedMachine"]);
            return RedirectToAction("Index", user.lastView);
        }

        //First page
        public ActionResult Index() {

            //Already logged in, show View
            if (Session["user"] != null)
            {
                loggedInUser user = (Session["user"]) as loggedInUser;
                if (user.errorText != "") {
                    ViewBag.errorText = user.errorText;
                    user.errorText = "";
                    return View("ErrorView");
                }
                user.updateShift();
                s5Func.update5S(user);
                user.lastView = "Home";
                ViewBag.tot = s5Func.numTodo + s5Func.numDone;
                ViewBag.done = s5Func.numDone;
                return View(user);
            }

            //Get windows user and check database if that user is allowed to login
            string winuser = System.Environment.UserName;
            var myUser = (from u in context.myuser where u.winuser == winuser select u).FirstOrDefault();

            //Setup the user
            if (myUser != null) {
                loggedInUser user = new loggedInUser(myUser);
                Session["user"] = user;
                MachinePartialView();
                s5Func.update5S(user);
                if (user.errorText != "")
                {
                    ViewBag.errorText = user.errorText;
                    return View("ErrorView");
                }
                ViewBag.tot = s5Func.numTodo + s5Func.numDone;
                ViewBag.done = s5Func.numDone;
                return View(user);
            }

            //If you where not authurized to login on that computer, send error View.
            ViewBag.errorText = "You are not authorized to login in and/or the user database is inaccessable";
            return View("ErrorView");
        }
    }
}