
namespace SecondTry.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("SettingFrequens")]
    public class SettingFrequens
    {
        public SettingFrequens()
        {
            this.Active = 0;
            this.WeekFrequens = 1;
        }

        [Key, Column(Order = 1)]
        public int DetailSettingID { get; set; }
        [Key, Column(Order = 2)]
        public byte ShortShift { get; set; }
        public byte Active { get; set; }
        public byte WeekFrequens { get; set; }
    }
}