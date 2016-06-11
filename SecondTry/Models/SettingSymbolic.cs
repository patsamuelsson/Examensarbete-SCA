namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("SettingSymbolic")]
    public class SettingSymbolic
    {
        [Key]
        public int MACHSYMBOLICID { get; set; }
        public byte Machno { get; set; }
        public string PROCUNIT { get; set; }
        public string TEXTGB { get; set; }
        public string TEXTSE { get; set; }
        public int BaseSettingID { get; set; }
    }
}