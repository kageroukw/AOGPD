using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AOGPD.Models
{
    public class Bolo
    {
        [Key]
        [ForeignKey("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "License Plate")]
        [StringLength(8, ErrorMessage = "Your License Plate can't be that long.")]
        public string LicensePlate { get; set; }

        [Display(Name = "Vehicle Name")]
        [StringLength(30, ErrorMessage = "Vehicle name can't be that long")]
        public string VehicleName { get; set; }

        [Display(Name = "Vehicle Color")]
        [StringLength(30, ErrorMessage = "Vehicle color can't be that long")]
        public string VehicleColor { get; set; }

        [Display(Name = "Wanted For")]
        public string WantedFor { get; set; }
    }
}