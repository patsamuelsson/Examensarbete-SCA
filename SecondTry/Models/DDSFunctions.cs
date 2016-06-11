using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondTry.Models
{
    public class DDSFunctions
    {
        dbContext context;
        public List<SettingExecutedReadings> DDSDone;
        public List<SettingExecutedReadings> DDSTodo;
        public int numDDSTodo;

        public DDSFunctions() {
            this.context = new dbContext();
        }

        public List<SettingExecutedReadings> getDDS(int shiftno, int MachNo) {
            var dds = (from s in context.SettingExecutedReadings where s.MACHNO == MachNo && s.shiftno == shiftno select s).ToList();
            return dds;
        }

        public void updateDDS(int MachNo, int shiftNo)
        {
            DDSTodo = (from s in context.SettingExecutedReadings where s.MACHNO == MachNo && s.shiftno == shiftNo && s.ReadingValue == null select s).ToList();
            DDSDone = (from s in context.SettingExecutedReadings where s.MACHNO == MachNo && s.shiftno == shiftNo && s.ReadingValue != null select s).ToList();
            numDDSTodo = DDSTodo.Count();
        }

        public void submitDDS(string id, string value, string comment)
        {
            int nId = int.Parse(id);
            double dValue = double.Parse(value);

            var executedDDS = (from s in context.SettingExecutedReadings where s.ExecutedReadingsID == nId select s).First();
            executedDDS.ReadingValue = dValue;
            executedDDS.Comment = comment;
            context.SaveChanges();

        }
    }
}