using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOGPD.Database;
using AOGPD.Models;
using Microsoft.AspNetCore.Mvc;
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
                _ctx.Add(civilianCharacter);
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
                _ctx.Add(civilianPlate);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(plate));
            }

            return View();
        }
    }
}