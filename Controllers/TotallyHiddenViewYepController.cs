using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AOGPD.Models;
using AOGPD.Database;

namespace AOGPD.Controllers
{
    public class TotallyHiddenViewYepController : Controller
    {
        private readonly ILogger<TotallyHiddenViewYepController> _logger;
        private readonly AOGDbContext _ctx;

        public TotallyHiddenViewYepController(ILogger<TotallyHiddenViewYepController> logger, AOGDbContext context)
        {
            _logger = logger;
            _ctx = context;
        }

        public async Task<IActionResult> civilians()
        {
            return View(await _ctx.Character.ToListAsync());
        }

        public async Task<IActionResult> plates()
        {
            return View(await _ctx.LicensePlate.ToListAsync());
        }

        public IActionResult create()
            => View();

        public IActionResult newsadd()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> newdispatch(Dispatcher dispatcher)
        {
            if (ModelState.IsValid)
            {
                byte[] data = Encoding.ASCII.GetBytes(dispatcher.Password);
                data = new SHA256Managed().ComputeHash(data);
                string hash = Encoding.ASCII.GetString(data);

                dispatcher.Password = hash;

                _ctx.Add(dispatcher);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(create));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addnews(News news)
        {
            if (ModelState.IsValid)
            {
                _ctx.Add(news);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(newsadd));
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
