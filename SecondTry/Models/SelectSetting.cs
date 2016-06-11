using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondTry.Models
{
    public class SelectSetting
    {
        public IEnumerable<SelectListItem> distinctProcFunc { get; set; }
        public IQueryable<SettingBaseProcess> procUnitofSelected { get; set; }
        public SettingBaseProcess choosenSetting { get; set; }
        public List<string> choosenSettingDescriptions = new List<string>();
        public List<int> choosenSettingMachSettId = new List<int>();
        public List<string> shortShift = new List<string>();
        private dbContext context = new dbContext();
        public int Machno { get; set; }

        public SelectSetting() {
            for (int x = 1; x < 8; x++) {
                for (int y = 1; y < 4; y++) {
                    shortShift.Add(x.ToString() + y.ToString());
                }
            }
        }

        public IEnumerable<SelectListItem> makeDistinctProcFunc() {
            var procFunc = (from d in context.SettingBaseProcess where d.Machno == Machno select d).Select(s => s.PROCFUNC).Distinct();

            distinctProcFunc = (procFunc.Select(c => new SelectListItem
            {
                Text = c,
                Value = c
            }));
            return distinctProcFunc;
        }

        public IQueryable<SettingBaseProcess> getProcUnitofSelected(string procFunc) {
            procUnitofSelected = (from d in context.SettingBaseProcess where d.Machno == Machno && d.PROCFUNC == procFunc select d).OrderBy(c => c.PROCUNIT);
            return procUnitofSelected;          
        }

        public List<string> getAllthemSetting(int index, int machno) {
            choosenSetting = procUnitofSelected.Skip(index).FirstOrDefault();
            var allSettings = (from d in context.SettingSymbolic where d.Machno == machno && d.BaseSettingID == choosenSetting.BaseSettingID select d).OrderBy(s => s.TEXTGB).ToList();
            var allAvailable = (from d in context.SettingAvailableSymbolic where d.Machno == machno && d.BaseSettingID == choosenSetting.BaseSettingID select d.MACHSYMBOLICID);
            choosenSettingDescriptions.Clear();
            choosenSettingMachSettId.Clear();
            foreach (SettingSymbolic sett in allSettings) {
                if (allAvailable.Contains(sett.MACHSYMBOLICID))
                {
                    choosenSettingDescriptions.Add(sett.TEXTGB + " (*)");
                }
                else {
                    choosenSettingDescriptions.Add(sett.TEXTGB);
                }
                choosenSettingMachSettId.Add(sett.MACHSYMBOLICID);
            }

            return choosenSettingDescriptions;
        }

        public void addDescription(int machno, string procUnit, string descSV, string descGB) {
            int baseSetting = choosenSetting.BaseSettingID;
            SettingSymbolic setting = new SettingSymbolic() {
                Machno = Convert.ToByte(machno),
                PROCUNIT = procUnit,
                TEXTGB = descGB,
                TEXTSE = descSV,
                BaseSettingID = baseSetting
            };

            context.SettingSymbolic.Add(setting);
            context.SaveChanges();
        }

        public void addSettings(DateTime startDate, int index, string user, int machno) {
            int baseSetting = choosenSetting.BaseSettingID;
            int machSymbolicId = choosenSettingMachSettId.Skip(index).FirstOrDefault();
            SettingDetailProcess detailProcess = new SettingDetailProcess()
            {
                BaseSettingId = baseSetting,
                MachSymbolicId = machSymbolicId,
                StartDate = startDate,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = user
            };
            context.SettingDetailProcess.Add(detailProcess);
            context.SaveChanges();

            foreach (string shift in shortShift) {
                byte test = Convert.ToByte(shift);
                var frequens = new SettingFrequens()
                {
                    DetailSettingID = detailProcess.DetailSettingId,
                    ShortShift = test
                };
                context.SettingFrequens.Add(frequens);
            }
            context.SaveChanges();
        }

        public void addValues(FormCollection form, loggedInUser user) {
            double minVal = double.Parse(form["minVal"]);
            double tarVal = double.Parse(form["targetVal"]);
            double maxVal = double.Parse(form["maxVal"]);
            string unit = form["unitVal"];
            int artNo = int.Parse(form["artNo"]);
            int facingVal = int.Parse(form["facingVal"]);
            int matNo = int.Parse(form["matNo"]);
            int prodInStapleVal = int.Parse(form["prodInStapleVal"]);
            int staplesInBagVal = int.Parse(form["staplesInBagVal"]);
            int articleSizeVal = int.Parse(form["articleSizeVal"]);
            int numofDepVal = 0;
            string deviantValueText = form["actionsIfDeviantVal"];

            int[] tempArray = new int[] { artNo, facingVal, matNo, prodInStapleVal, staplesInBagVal, articleSizeVal };
            foreach (int num in tempArray) {
                if (num > 0) {
                    numofDepVal++;
                }
            }

            string indexoredit = form["setValuesIndex"];
            if (indexoredit != "editValue")
            {
                int index = int.Parse(indexoredit);
                int machSymbolicId = choosenSettingMachSettId.Skip(index).FirstOrDefault();
                int DetailSettingId = (from s in context.SettingDetailProcess where s.MachSymbolicId == machSymbolicId select s.DetailSettingId).FirstOrDefault();

                SettingValue settVal = new SettingValue()
                {
                    DetailSettingID = DetailSettingId,
                    MinValue = minVal,
                    TargetValue = tarVal,
                    MaxValue = maxVal,
                    Unit = unit,
                    ValidFrom = user.currShift.shiftno,
                    ValidTo = 999999993,
                    LastUpdatedBy = user.winuser,
                    Deleted = 0,
                    ArtNo = artNo,
                    Facing = facingVal,
                    MaterialNo = matNo,
                    ProdsInStaple = prodInStapleVal,
                    StaplesInBag = staplesInBagVal,
                    ArticleSize = articleSizeVal,
                    NumOfDependent = numofDepVal,
                    ActionsIfDeviantValue = deviantValueText
                };
                context.SettingValue.Add(settVal);
            }
            else {
                int settingID = int.Parse(form["settingID"]);
                SettingValue theSetting = (from s in context.SettingValue where s.SettingValuesID == settingID select s).FirstOrDefault();
                theSetting.MinValue = minVal;
                theSetting.TargetValue = tarVal;
                theSetting.MaxValue = maxVal;
                theSetting.Unit = unit;
                theSetting.ValidFrom = user.currShift.shiftno;
                theSetting.ValidTo = 999999993;
                theSetting.LastUpdatedBy = user.winuser;
                theSetting.Deleted = 0;
                theSetting.ArtNo = artNo;
                theSetting.Facing = facingVal;
                theSetting.MaterialNo = matNo;
                theSetting.ProdsInStaple = prodInStapleVal;
                theSetting.StaplesInBag = staplesInBagVal;
                theSetting.ArticleSize = articleSizeVal;
                theSetting.NumOfDependent = numofDepVal;
                theSetting.ActionsIfDeviantValue = deviantValueText;
            }
            context.SaveChanges();
        }

        public List<SettingValue> getAllSettingValues(int index)
        {
            int machSymbolicId = choosenSettingMachSettId.Skip(index).FirstOrDefault();
            int detailSettingId = (from s in context.SettingDetailProcess where s.MachSymbolicId == machSymbolicId select s.DetailSettingId).FirstOrDefault();
            var allSettingValues = (from s in context.SettingValue where s.DetailSettingID == detailSettingId select s).ToList();
            return allSettingValues;
        }
    }
}