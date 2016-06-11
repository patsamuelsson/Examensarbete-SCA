using SecondTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondTry.Controllers
{
    public class AdminController : Controller
    {
        dbContext context = new dbContext();

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

                //Check so the user is the correct userlevel to access admin
                if (user.userlevel != "rst")
                {
                    return RedirectToAction("Index", user.lastView);
                }

                //If there is an Errortext return the ErrorView
                if (user.errorText != "")
                {
                    ViewBag.errorText = user.errorText;
                    user.errorText = "";
                    return View("ErrorView");
                }

                user.lastView = "Admin";
                user.updateShift();
                user.selectSetting.Machno = user.Machno;
                ViewBag.procFunc = user.selectSetting.makeDistinctProcFunc();
                return View(user.selectSetting);
            }
        }

        public ActionResult DDSFreq()
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

                //Check so the user is the correct userlevel to access admin
                if (user.userlevel != "rst")
                {
                    return RedirectToAction("Index", user.lastView);
                }

                //If there is an Errortext return the ErrorView
                if (user.errorText != "")
                {
                    ViewBag.errorText = user.errorText;
                    user.errorText = "";
                    return View("ErrorView");
                }

                user.lastView = "Admin";
                user.updateShift();
                var allActiveMachineSettings = new SettingFrequensFunctions();
                return View(allActiveMachineSettings.getAllConnections(user.Machno));
            }
        }

        public ActionResult ProcUnit(string procFunc)
        {
            loggedInUser user = Session["user"] as loggedInUser;
            var procUnit = user.selectSetting.getProcUnitofSelected(procFunc);
            List<string> procUnitList = new List<string>();
            foreach (var sett in procUnit)
            {
                if (sett.XPOS == null)
                {
                    procUnitList.Add(sett.PROCUNIT + " (YPOS: " + sett.YPOS + ")");
                }
                else if (sett.YPOS == null)
                {
                    procUnitList.Add(sett.PROCUNIT + " (XPOS: " + sett.XPOS + ")");
                }
                else
                {
                    procUnitList.Add(sett.PROCUNIT + " (XPOS: " + sett.XPOS + " YPOS: " + sett.YPOS + ")");
                }
            }
            return Json(procUnitList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getDescriptions(string index)
        {
            loggedInUser user = Session["user"] as loggedInUser;
            List<string> descriptions = user.selectSetting.getAllthemSetting(int.Parse(index), user.Machno);
            return Json(descriptions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getAllSettingValues(int index)
        {
            loggedInUser user = Session["user"] as loggedInUser;
            var descValues = user.selectSetting.getAllSettingValues(index);
            return Json(descValues, JsonRequestBehavior.AllowGet);
        }

        public void setActiveSettingFrequens(int id, byte shortShift, byte active) {

            var allActiveMachineSettings = new SettingFrequensFunctions();
            allActiveMachineSettings.updateDbSettingFrequensActive(id, shortShift, active);
        }

        public void setWeekSettingFrequens(int id, byte weekFreq) {
            var allActiveMachineSettings = new SettingFrequensFunctions();
            allActiveMachineSettings.updateDbWeekSettingFrequens(id, weekFreq);
        }

        [HttpPost]
        public ActionResult addDescription()
        {
            loggedInUser user = Session["user"] as loggedInUser;
            var procUnit = Request["hiddUnit"];
            int index = procUnit.IndexOf(" ");

            //procUnit request contains XPOS/YPOS so we remove everything behind the first space
            if (index > 0)
            {
                procUnit = procUnit.Substring(0, index);
            }
            var descGB = Request["descGB"];
            var descSV = Request["descSV"];
            user.selectSetting.addDescription(user.Machno, procUnit, descSV, descGB);
            return RedirectToAction("Index", user.lastView);
        }

        public ActionResult addSetting()
        {
            loggedInUser user = Session["user"] as loggedInUser;
            var startDate = DateTime.ParseExact(Request["startDate"], "yyyy-MM-dd", null);
            var week = Request["weekpicker"];
            int indexSelectedDesc = int.Parse(Request["indexSelectedDesc"]);
            user.selectSetting.addSettings(startDate, indexSelectedDesc, user.winuser, user.Machno);
            return RedirectToAction("Index", user.lastView);
        }

        [HttpPost]
        public ActionResult addValues(FormCollection form)
        {
            loggedInUser user = Session["user"] as loggedInUser;
            user.selectSetting.addValues(form, user);
            return RedirectToAction("Index", user.lastView);
        }
    }
}