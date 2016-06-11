
namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("V_CommonMachineShift")]
    public class MachineShift
    {
        [Key]
        public string DESCRIPTION { get; set; }
        public byte MACHNO { get; set; }
        public short schemecd { get; set; }
        public DateTime shiftdate { get; set; }
        public DateTime shiftbegtime { get; set; }
        public Nullable<DateTime> shiftendtime { get; set; }
        public string shift { get; set; }
        public int shiftno { get; set; }
        public string ENDSHIFT { get; set; }
        public string STARTSHIFT { get; set; }
    }
}
