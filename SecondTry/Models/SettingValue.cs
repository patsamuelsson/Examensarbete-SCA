namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("SettingValue")]
    public class SettingValue
    {
        [Key]
        public int SettingValuesID { get; set; }
        public int DetailSettingID { get; set; }
        public double MinValue { get; set; }
        public double TargetValue { get; set; }
        public double MaxValue { get; set; }
        public string Unit { get; set; }
        public int ValidFrom { get; set; }
        public int ValidTo { get; set; }
        public string LastUpdatedBy { get; set; }
        public Byte Deleted { get; set; }
        public int ArtNo { get; set; }
        public int MaterialNo { get; set; }
        public int Facing { get; set; }
        public int ProdsInStaple { get; set; }
        public int StaplesInBag { get; set; }
        public int ArticleSize { get; set; }
        public int NumOfDependent { get; set; }
        public string ActionsIfDeviantValue { get; set; }
    }
}