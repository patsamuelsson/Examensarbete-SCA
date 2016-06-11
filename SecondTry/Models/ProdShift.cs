
namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("V_productionShift_Test")]
    public class ProdShift
    {
        [Key]
        public string Shift { get; set; }
        public string Machine { get; set; }
        public int ShiftNo { get; set; }
        public int StopsUnPlanned { get; set; }
        public int Wastetot { get; set; }
        public int ApprovedQTY { get; set; }
        public double ProdHourNet { get; set; }
        public double BudMachSpeed { get; set; }
        public Int16 ShiftSumTargetTime { get; set; }
        public double OEETarget { get; set; }
        public double UHHour { get; set; }
        public Int16 ExtraStop { get; set; }
        public double stop8h { get; set; }
        public double WasteTarget { get; set; }
        public double ChangeOverExtraWaste { get; set; }
        public double UHExtraWaste { get; set; }
    }
}