namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("V_SettingExecutedReadings")]
    public class SettingExecutedReadings
    {
        [Key]
        public int ExecutedReadingsID { get; set; }
        public string shift { get; set; }
        public string DESCRIPTION { get; set; }
        public byte MACHNO { get; set; }
        public int SettingValuesID { get; set; }
        public int shiftno { get; set; }
        public double? ReadingValue { get; set; }
        public double MinValue { get; set; }
        public double TargetValue { get; set; }
        public double MaxValue { get; set; }
        public string Unit { get; set; }
        public string TEXTGB { get; set; }
        public string PROCFUNC { get; set; }
        public string PROCUNIT { get; set; }
        public string XPOS { get; set; }
        public string YPOS { get; set; }
        public string ActionsIfDeviantValue { get; set; }
        public string Comment { get; set; }
    }
}