using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondTry.Models
{
    /*
        Class for collecting all info that will be shown
        in MachinePartialView.
    */
    public class machineTargetResult
    {
        public string oeeResult { get; set; }
        public string wasteResult { get; set; }
        public string stopResult { get; set; }

        public double oeeTarget { get; set; }
        public double wasteTarget { get; set; }
        public double stopTarget { get; set; }

        public string oeeColor { get; set; }
        public string wasteColor { get; set; }
        public string stopColor { get; set; }

    }
}