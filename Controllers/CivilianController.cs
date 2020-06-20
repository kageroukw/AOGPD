using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AOGPD.Database;
using AOGPD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AOGPD.Controllers
{
    public class CivilianController : Controller
    {
        private readonly ILogger<CivilianController> _logger;
        private readonly AOGDbContext _ctx;

        public CivilianController(ILogger<CivilianController> logger, AOGDbContext context)
        {
            _logger = logger;
            _ctx = context;
        }

        public IActionResult civilian()
            => View();

        public IActionResult plate()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> civilian(CivilianCharacter civilianCharacter)
        {
            if (ModelState.IsValid)
            {
                var uppercaseCharacter = new CivilianCharacter
                {
                    FirstName = civilianCharacter.FirstName.ToUpper(),
                    LastName = civilianCharacter.LastName.ToUpper(),
                    DateofBirth = civilianCharacter.DateofBirth,
                    Citations = civilianCharacter.Citations,
                    Wanted = civilianCharacter.Wanted,
                };

                var oldcivi = await _ctx.Character
                    .Where(x => x.FirstName == uppercaseCharacter.FirstName && x.LastName == uppercaseCharacter.LastName)
                    .FirstOrDefaultAsync();

                if (oldcivi != null)
                {
                    _ctx.Character.Remove(oldcivi);
                }

                _ctx.Add(uppercaseCharacter);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(civilian));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> plate(CivilianLicensePlate civilianPlate)
        {
            if (ModelState.IsValid)
            {
                var uppercasePlate = new CivilianLicensePlate
                {
                    LicensePlate = civilianPlate.LicensePlate.ToUpper(),
                    PlateOwner = civilianPlate.PlateOwner.ToUpper(),
                    Registration = civilianPlate.Registration,
                    Insurance = civilianPlate.Insurance,
                    Additional = civilianPlate.Additional.ToUpper(),
                    VehicleName = civilianPlate.VehicleName.ToUpper(),
                    VehicleColor = civilianPlate.VehicleColor.ToUpper(),
                    AdditionalVehicleDetails = civilianPlate.AdditionalVehicleDetails.ToUpper()
                };

                var oldplate = await _ctx.LicensePlate
                    .Where(x => x.LicensePlate == uppercasePlate.LicensePlate)
                    .FirstOrDefaultAsync();

                if (oldplate != null)
                {
                    _ctx.LicensePlate.Remove(oldplate);
                }

                _ctx.Add(uppercasePlate);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(plate));
            }

            return View();
        }
    }
}