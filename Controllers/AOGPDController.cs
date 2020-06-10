using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AOGPD.Models;
using AOGPD.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.PortableExecutable;

namespace AOGPD.Controllers
{
    public class AOGPDController : Controller
    {
        private readonly ILogger<AOGPDController> _logger;
        private readonly AOGDbContext _ctx;

        public AOGPDController(ILogger<AOGPDController> logger, AOGDbContext context)
        {
            _logger = logger;
            _ctx = context;
        }

        // Views
        public IActionResult civilian()
            => View();

        public IActionResult plate()
            => View();

        public IActionResult police()
            => View();

        public IActionResult nodata()
            => View();

        public IActionResult about()
            => View();

        // Register Civilian/Vehicle Plate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> civilian(CivilianCharacter civilianCharacter)
        {
            if (ModelState.IsValid)
            {
                _ctx.Add(civilianCharacter);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(civilian));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> registerplate(string civilianLicense)
        {
            if (ModelState.IsValid)
            {
                _ctx.Add(civilianLicense);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(plate));
            }

            return View("plate");
        }

        // Searches
        [HttpGet]
        public async Task<IActionResult> lookup(string firstName, string lastName)
        {
            if (ModelState.IsValid)
            {
                var civi = await _ctx.Character
                    .Where(x => x.FirstName == firstName && x.LastName == lastName)
                    .FirstOrDefaultAsync();

                if (civi == null)
                {
                    return RedirectToAction(nameof(nodata));
                }

                ViewBag.fName = civi.FirstName;
                ViewBag.lName = civi.LastName;
                ViewBag.dob = civi.DateofBirth;
                ViewBag.citations = civi.Citations;
                ViewBag.wanted = civi.Wanted;
            }

            return View("police");
        }

        [HttpGet]
        public async Task<IActionResult> platelookup(string license)
        {
            if (ModelState.IsValid)
            {
                var plate = await _ctx.LicensePlate
                    .Where(x => x.LicensePlate == license)
                    .FirstOrDefaultAsync();

                if (plate == null)
                {
                    return RedirectToAction(nameof(nodata));
                }

                ViewBag.fName = plate.LicensePlate;
                ViewBag.lName = plate.PlateOwner;
                ViewBag.dob = plate.Registration;
                ViewBag.citations = plate.Insurance;
                ViewBag.wanted = plate.Additional;
            }

            return View("police");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}