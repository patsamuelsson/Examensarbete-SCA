
namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("SettingBaseProcess")]
    public class SettingBaseProcess
    {
        [Key]
        public int BaseSettingID { get; set; }
        public byte Machno { get; set; }
        public string PROCFUNC { get; set; }
        public string PROCUNIT { get; set; }
        public string XPOS { get; set; }
        public string YPOS { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public byte Deleted { get; set; }
    }
}