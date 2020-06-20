using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AOGPD.Models
{
    public class News
    {
        [Key]
        [ForeignKey("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Header")]
        public string Header { get; set; }

        [Display(Name = "Body")]
        public string Body { get; set; }

        [Display(Name = "Date")]
        public string PostedAt { get; set; }

        [Display(Name = "Snapshot")]
        public string Image { get; set; }
    }
}