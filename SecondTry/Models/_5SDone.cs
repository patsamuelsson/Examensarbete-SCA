namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("_5SDone")]
    public class _5SDone
    {
        [Key]
        public int Machine5SID { get; set; }
        public int Main5SListID { get; set; }
        public string shift { get; set; }
        public string Duration { get; set; }
        public int shiftno { get; set; }
        public string ShortShift { get; set; }
        public Boolean Performed { get; set; }
        public string PerformedBy { get; set; }
        public string sign { get; set; }
        public DateTime PerformedTime { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string HyperLink { get; set; }
    }
}