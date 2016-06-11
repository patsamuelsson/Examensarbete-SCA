namespace SecondTry.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("V_GetShift5S")]
    public class GetShift5S
    {
        [Key]
        public int _5SDoneID { get; set; }
        public int ID { get; set; }
        public string _5SDescription { get; set; }
        public string shift { get; set; }
        public string Duration { get; set; }
        public string Machine { get; set; }
        public Boolean Performed { get; set; }
        public string PerformedBy { get; set; }
        public Nullable<DateTime> PerformedTime { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string HyperLink { get; set; } 
        public string HyperLinkCorrect { get; set; }
    }
}