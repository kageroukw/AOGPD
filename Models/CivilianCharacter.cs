using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOGPD.Models
{
    public class CivilianCharacter
    {
        [Key]
        [ForeignKey("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(15, ErrorMessage = "Your First Name can't be that long.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(15, ErrorMessage = "Your Last Name can't be that long.")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "Citations")]
        public int Citations { get; set; }

        [Display(Name = "Wanted")]
        public bool Wanted { get; set; }
    }
}