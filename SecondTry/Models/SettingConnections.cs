namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("V_SettingConnections")]
    public class SettingConnections
    {
        [Key]
        public int SettingValuesID { get; set; }
        public int DetailSettingID { get; set; }
        public int MACHSYMBOLICID { get; set; }
        public int BaseSettingID { get; set; }
        public string TEXTGB { get; set; }
        public string TEXTSE { get; set; }
        public byte MACHNO { get; set; }
    }
}