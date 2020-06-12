using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AOGPD.Models
{
    public class PenalCode
    {
        [Key]
        [ForeignKey("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Penal Code")]
        public string Code { get; set; }

        [Display(Name = "Description")]
        public string PenalCodeDescription { get; set; }

        [Display(Name = "Penal Time")]
        public string PenalTime { get; set; }
    }
}