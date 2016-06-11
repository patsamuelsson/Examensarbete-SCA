using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SecondTry.Models
{
    [Table("myuser")]
    public class myuser
    {
        [Key]
        public int ID { get; set; }
        public string username { get; set; }
        public string winuser { get; set; }
        public string userlevel { get; set; }
        public string userlanguage { get; set; }
        public string startsida { get; set; }
        public string startmaskin { get; set; }
        public Boolean locked { get; set; }
        public string domuserlevel { get; set; }

        /*public DateTime Dom_LoggedOn { get; set; }
        public DateTime Dom_LastSaved { get; set; }
        public string Dom_Open { get; set; }
        public string Dom_Doc { get; set; }
        public DateTime P_Doc_LoggedOn { get; set; }
        public DateTime P_Doc_LastSaved { get; set; }
        public string P_Doc_Open { get; set; }
        public string P_Doc { get; set; }
        public string AccountType { get; set; }*/
    }
}