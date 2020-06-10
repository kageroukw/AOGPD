using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOGPD.Models;

namespace AOGPD.ViewModels
{
    public class PoliceViewModel
    {
        public CivilianCharacter Civilian { get; set; }
        public CivilianLicensePlate LicensePlate { get; set; }
        public Dispatcher Dispatcher { get; set; }
    }
}