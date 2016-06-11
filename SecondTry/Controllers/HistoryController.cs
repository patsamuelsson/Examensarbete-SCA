using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SecondTry.Models;
using System.Web.Helpers;

namespace SecondTry.Controllers
{
    public class HistoryController : Controller
    {
        dbContext context = new dbContext();
        public ActionResult Index()
        {
            loggedInUser user = Session["user"] as loggedInUser;
            user.lastView = "History";
            /*var fetchSetting = from d in context.MachSettHistory where d.machSettID == 1 select d;
            string settName = fetchSetting.First().MachineSettings.machineSetting;
            var chart = new Chart(width: 600, height: 400, theme: ChartTheme.Green)
                .AddTitle(settName).AddSeries("Default", chartType: "line",
                xValue: fetchSetting, xField: "date",
                yValues: fetchSetting, yFields: "paramInput"
                ).Save(path: "~/content/chart.png");

            IEnumerable<SelectListItem> items = context.MachineSettings.Select(c => new SelectListItem
            {
                Value = c.machSettID.ToString(),
                Text = c.machineSetting
            });

            ViewBag.machSettID = items;

            if (Session["user"] == null)
            {
                return RedirectToAction("Index");
            }
            return View(fetchSetting.ToList());*/
            return View();
        }
        /*
        [HttpPost]
        public ActionResult Index(string idt)
        {
            int id = int.Parse(Request["machSettID"]);

            var fetchSetting = from d in context.MachSettHistory where d.machSettID == id select d;

            string settName = fetchSetting.First().MachineSettings.machineSetting;
            var chart = new Chart(width: 600, height: 400, theme: ChartTheme.Green)
                .AddTitle(settName).AddSeries("Default", chartType: "line",
                xValue: fetchSetting, xField: "date",
                yValues: fetchSetting, yFields: "paramInput"
                ).Save(path: "~/content/chart.png");

            IEnumerable<SelectListItem> items = context.MachineSettings.Select(c => new SelectListItem
            {
                Value = c.machSettID.ToString(),
                Text = c.machineSetting
            });

            ViewBag.machSettID = items;

            return View(fetchSetting.ToList());
        }*/
    }
}