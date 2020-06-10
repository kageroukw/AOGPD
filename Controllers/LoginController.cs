using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AOGPD.Database;
using AOGPD.Models;

namespace AOGPD.Controllers
{
    [TypeFilter(typeof(AuthorizePageHandlerFilter))]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly AOGDbContext _ctx;

        public LoginController(ILogger<LoginController> logger, AOGDbContext context)
        {
            _logger = logger;
            _ctx = context;
        }

        public IActionResult index()
            => View();

        public IActionResult landing()
            => View();

        public IActionResult nodata()
            => View();

        [HttpGet]
        public async Task<IActionResult> login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                byte[] data = Encoding.ASCII.GetBytes(password);
                data = new SHA256Managed().ComputeHash(data);
                string hash = Encoding.ASCII.GetString(data);

                var disp = await _ctx.Dispatch
                    .Where(x => x.UserName == username && x.Password == hash)
                    .FirstOrDefaultAsync();

                if (disp == null)
                {
                    return RedirectToAction(nameof(nodata));
                }

                return RedirectToAction("index", "dispatch");
            }

            return View("index");
        }
    }
}