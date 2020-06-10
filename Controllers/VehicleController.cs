using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AOGPD.Models;
using AOGPD.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AOGPD.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly AOGDbContext _ctx;

        public VehicleController(ILogger<VehicleController> logger, AOGDbContext context)
        {
            _logger = logger;
            _ctx = context;
        }

        public IActionResult plate()
            => View();

        public IActionResult policeplate()
            => View();

        public IActionResult nodata()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> plate(CivilianLicensePlate civilianLicense)
        {
            if (ModelState.IsValid)
            {
                _ctx.Add(civilianLicense);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(plate));
            }

            return View("plate");
        }

        [HttpGet]
        public async Task<IActionResult> lookup(string licensePlate)
        {
            if (ModelState.IsValid)
            {
                var plate = await _ctx.LicensePlate
                    .Where(x => x.LicensePlate == licensePlate)
                    .FirstOrDefaultAsync();

                if (plate == null)
                {
                    return RedirectToAction(nameof(nodata));
                }

                ViewBag.licPlate = plate.LicensePlate;
                ViewBag.pltO = plate.PlateOwner;
                ViewBag.reg = plate.Registration;
                ViewBag.ins = plate.Insurance;
                ViewBag.add = plate.Additional;
            }

            return View("policeplate");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
