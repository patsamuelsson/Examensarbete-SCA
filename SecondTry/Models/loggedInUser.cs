using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondTry.Models
{
    public class loggedInUser
    {
        public string username { get; set; }
        public string userlevel { get; set; }
        public Boolean locked { get; set; }
        public string winuser { get; set; }
        public string maskin { get; set; }

        public int Machno { get; set; }
        public string lastView { get; set; }
        public MachineShift currShift { get; set; }
        public MachineShift selShift { get; set; }
        private int lastShiftUpdate = 0;
        private dbContext context = new dbContext();
        public SelectSetting selectSetting = new SelectSetting();
        public string errorText;

        public loggedInUser(myuser user) {
            DateTime timeNow = DateTime.Now;
            this.errorText = "";
            this.currShift = (from s in context.MachineShift where s.shiftbegtime < timeNow && s.shiftendtime > timeNow && s.DESCRIPTION == user.startmaskin select s).First();
            this.selShift = this.currShift;
            this.locked = user.locked;
            this.username = user.username;
            this.maskin = user.startmaskin;
            this.userlevel = user.userlevel;
            this.Machno = currShift.MACHNO;
            this.lastView = "Home";
            this.winuser = user.winuser;
        }

        /*
            Checks to see if the shift has ended, 
            updates to the new one if the clock i 
            past 6:00/(14:00 or 15:00)/22:00
                                                    */
        public void updateShift()
        {
            DateTime now = DateTime.Now;
            if (lastShiftUpdate == 0)
            {
                lastShiftUpdate = now.Hour;
            }
            else {
                int hourNow = now.Hour;
                if (lastShiftUpdate == 23 && hourNow == 0)
                {
                    lastShiftUpdate = hourNow;
                    currShift = (from s in context.MachineShift where s.shiftbegtime < now && s.shiftendtime > now && s.DESCRIPTION == this.maskin select s).First();
                }else if (hourNow > lastShiftUpdate) {
                    lastShiftUpdate = hourNow;
                    currShift = (from s in context.MachineShift where s.shiftbegtime < now && s.shiftendtime > now && s.DESCRIPTION == this.maskin select s).First();
                }
            }
        }

        /* Get all the shifts from now 
           and back the amount of days
           specified in daysBack.      */
        public IEnumerable<SelectListItem> getShiftBack(int daysBack) {
            DateTime timeNow = DateTime.Now;
            DateTime timeBack = timeNow.AddDays((daysBack*(-1)));
            var allShift = from d in context.MachineShift where d.DESCRIPTION == this.maskin && d.shiftbegtime > timeBack && d.shiftbegtime < timeNow select d;
            var allShiftList = (allShift.Select(c => new SelectListItem
            {
                Value = c.shiftno.ToString(),
                Text = c.shift,
            })).OrderByDescending(c => c.Value);
            return allShiftList;
        }

        public SelectList allMachines() {
            var theList = from l in context.Machine where l.DDS == 1 select l;
            var selectList = (theList.Select(c => new SelectListItem
            {
                Text = c.DESCRIPTION,
                Value = c.MACHNO.ToString()
            }));
            int selectedIndex = theList.Where(s => s.DESCRIPTION == this.maskin).First().MACHNO;
            var test = new SelectList(selectList, "Value", "Text", selectedIndex);
            return test;
        }

        public void changeMachine(string selectedValue) {
            this.maskin = this.allMachines().Where(s => s.Value == selectedValue).First().Text;
            this.Machno = int.Parse(selectedValue);
            DateTime timeNow = DateTime.Now;
            this.currShift = (from s in context.MachineShift where s.shiftbegtime < timeNow && s.shiftendtime > timeNow && s.DESCRIPTION == this.maskin select s).First();
            this.selShift = this.currShift;
        }

        public Boolean prodMachine() {
            var t = (from p in context.Machine where p.DESCRIPTION == this.maskin select p.ProducingMachine).First();
            if (t == 0)
            {
                return false;
            }
            else {
                return true;
            }
        }
    }
}