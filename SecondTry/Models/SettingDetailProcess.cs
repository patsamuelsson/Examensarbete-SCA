
namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("SettingDetailProcess")]
    public class SettingDetailProcess
    {
        [Key]
        public int DetailSettingId { get; set; }
        public int BaseSettingId { get; set; }
        public int MachSymbolicId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public int SortOrder { get; set; }
        public byte ChangeOver { get; set; }
    }
}