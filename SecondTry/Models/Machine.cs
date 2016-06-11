using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SecondTry.Models
{
    [Table("MACHINE")]
    public class Machine
    {
        [Key]
        public byte MACHNO { get; set; }
        public string DESCRIPTION { get; set; }
        public byte DDS { get; set; }
        public byte ProducingMachine { get; set; }
    }
}