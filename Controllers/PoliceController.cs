using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AOGPD.Database;
using AOGPD.ViewModels;
using AOGPD.Models;
using System.Runtime.CompilerServices;

namespace AOGPD.Controllers
{
    public class PoliceController : Controller
    {
        private readonly ILogger<PoliceController> _logger;
        private readonly AOGDbContext _ctx;

        public PoliceController(ILogger<PoliceController> logger, AOGDbContext context)
        {
            _logger = logger;
            _ctx = context;
        }

        public async Task<IActionResult> index()
        {
            return View(await _ctx.Bolo.ToListAsync());
        }

        public IActionResult civilian()
            => View();

        public IActionResult plate()
            => View();

        public IActionResult bolo()
            => View();

        public IActionResult tencodes()
            => View();

        public IActionResult penalcode()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> bolo(Bolo bolo)
        {
            if (ModelState.IsValid)
            {
                var uppercaseBolo = new Bolo
                {
                    LicensePlate = bolo.LicensePlate.ToUpper(),
                    VehicleName = bolo.VehicleName.ToUpper(),
                    VehicleColor = bolo.VehicleColor.ToUpper(),
                    WantedFor = bolo.WantedFor.ToUpper()
                };

                var oldbolo = await _ctx.Bolo
                    .Where(x => x.LicensePlate == uppercaseBolo.LicensePlate)
                    .FirstOrDefaultAsync();

                if (oldbolo != null)
                {
                    _ctx.Bolo.Remove(oldbolo);
                }

                _ctx.Add(uppercaseBolo);
                await _ctx.SaveChangesAsync();

                return RedirectToAction(nameof(index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> lookup(string firstName, string lastName)
        {
            if (ModelState.IsValid)
            {
                var civi = await _ctx.Character
                    .Where(x => x.FirstName == firstName.ToUpper() && x.LastName == lastName.ToUpper())
                    .FirstOrDefaultAsync();

                if (civi == null)
                {
                    return RedirectToAction("nodata", "aogpd");
                }

                ViewBag.fName = civi.FirstName;
                ViewBag.lName = civi.LastName;
                ViewBag.dob = civi.DateofBirth;
                ViewBag.citations = civi.Citations;
                ViewBag.wanted = civi.Wanted;
            }

            return View("civilian");
        }

        [HttpGet]
        public async Task<IActionResult> platelookup(string licensePlate)
        {
            if (ModelState.IsValid)
            {
                var plate = await _ctx.LicensePlate
                    .Where(x => x.LicensePlate == licensePlate.ToUpper())
                    .FirstOrDefaultAsync();

                if (plate == null)
                {
                    return RedirectToAction("nodata", "aogpd");
                }

                ViewBag.vicN = plate.VehicleName;
                ViewBag.vicC = plate.VehicleColor;
                ViewBag.vicAD = plate.AdditionalVehicleDetails;
                ViewBag.licPlate = plate.LicensePlate;
                ViewBag.pltO = plate.PlateOwner;
                ViewBag.reg = plate.Registration;
                ViewBag.ins = plate.Insurance;
                ViewBag.add = plate.Additional;
            }

            return View("plate");
        }

        public async Task<IActionResult> delete(int? id)
        {
            var bolo = await _ctx.Bolo.FirstOrDefaultAsync(x => x.Id == id);
            if (bolo == null)
            {
                return RedirectToAction("nodata", "aogpd");
            }

            return View(bolo);
        }

        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> deleteconfirmed(int id)
        {
            var bolo = await _ctx.Bolo.FindAsync(id);
            _ctx.Bolo.Remove(bolo);
            await _ctx.SaveChangesAsync();

            return RedirectToAction(nameof(index));
        }
    }
}