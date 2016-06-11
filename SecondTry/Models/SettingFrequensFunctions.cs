using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondTry.Models
{
    public class SettingFrequensFunctions
    {

        dbContext context = new dbContext();

        public List<FrequensViewModel> getAllConnections(int machNo)
        {
            byte mach = Convert.ToByte(machNo);
            var allMachStartSett = (from s in context.SettingConnections where s.MACHNO == mach select s).OrderBy(s => s.DetailSettingID).ToList();
            int oldDetailId = -1;
            List<int> distinctID = new List<int>();
            foreach (var id in allMachStartSett) {
                if (oldDetailId != id.DetailSettingID) {
                    distinctID.Add(id.DetailSettingID);
                    oldDetailId = id.DetailSettingID;
                }
            }

            List<FrequensViewModel> finalFrequensList = new List<FrequensViewModel>();
            foreach (var id in distinctID) {
                var allFrequens = (from s in context.SettingFrequens where s.DetailSettingID == id select s).ToList();
                var currentSetting = allMachStartSett.Where(s => s.DetailSettingID == id).First();
                foreach (var freq in allFrequens) {
                    string active = "";
                    if(freq.Active == 1)
                    {
                        active = "checked";
                    }
                    finalFrequensList.Add(new FrequensViewModel() {
                        MachSymbolicID = currentSetting.MACHSYMBOLICID,
                        DetailSettingID = id,
                        description = currentSetting.TEXTGB,
                        ShortShift = freq.ShortShift,
                        Active = active,
                        WeekFrequens = freq.WeekFrequens
                    });
                }
            }
            return finalFrequensList;
        }

        public void updateDbSettingFrequensActive(int id, byte shortShift, byte active) {
            var freqToChange = (from s in context.SettingFrequens where s.DetailSettingID == id && s.ShortShift == shortShift select s).First();
            freqToChange.Active = active;
            context.SaveChanges();
        }

        public void updateDbWeekSettingFrequens(int id, byte weekFreq)
        {
            var freqToChange = (from s in context.SettingFrequens where s.DetailSettingID == id select s);
            foreach (var freq in freqToChange) {
                freq.WeekFrequens = weekFreq;
            }
            context.SaveChanges();
        }
    }
}