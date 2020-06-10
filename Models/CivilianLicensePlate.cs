using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOGPD.Models
{
    public class CivilianLicensePlate
    {
        [Key]
        [ForeignKey("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "License Plate")]
        [StringLength(8, ErrorMessage = "Your License Plate can't be that long.")]
        public string LicensePlate { get; set; }

        [Display(Name = "Plate Owner")]
        [StringLength(15, ErrorMessage = "Plate owner's name shouldn't be that long.")]
        public string PlateOwner { get; set; }

        [Display(Name = "Registration")]
        public string Registration { get; set; }

        [Display(Name = "Insurance")]
        public string Insurance { get; set; }

        [Display(Name = "Additional")]
        [StringLength(25, ErrorMessage = "Additional can't be that long.")]
        public string Additional { get; set; }

        [Display(Name = "Vehicle Name")]
        [StringLength(30, ErrorMessage = "Vehicle name can't be that long")]
        public string VehicleName { get; set; }

        [Display(Name = "Vehicle Color")]
        [StringLength(30, ErrorMessage = "Vehicle color can't be that long")]
        public string VehicleColor { get; set; }

        [Display(Name = "Additional Vehicle Details")]
        public string AdditionalVehicleDetails { get; set; }
    }
}