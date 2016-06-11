using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondTry.Models
{
    public class FrequensViewModel
    {
        public int MachSymbolicID { get; set; }
        public int DetailSettingID { get; set; }
        public string description { get; set; }
        public byte ShortShift { get; set; }
        public string Active { get; set; }
        public byte WeekFrequens { get; set; }
    }
}