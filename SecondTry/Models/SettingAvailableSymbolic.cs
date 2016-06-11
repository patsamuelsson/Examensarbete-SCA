namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("V_SettingAvalibleSymbolic")]
    public class SettingAvailableSymbolic
    {
        [Key]
        public byte Machno { get; set; }
        public string PROCFUNC { get; set; }
        public string PROCUNIT { get; set; }
        public string TEXTGB { get; set; }
        public int MACHSYMBOLICID { get; set; }
        public int BaseSettingID { get; set; }
        public string XPOS { get; set; }
        public string YPOS { get; set; }
    }
}