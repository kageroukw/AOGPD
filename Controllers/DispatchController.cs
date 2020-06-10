using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AOGPD.Database;

namespace AOGPD.Controllers
{
    public class DispatchController : Controller
    {
        private readonly ILogger<DispatchController> _logger;
        private readonly AOGDbContext _ctx;

        public DispatchController(ILogger<DispatchController> logger, AOGDbContext context)
        {
            _logger = logger;
            _ctx = context;
        }

        [Authorize]
        public IActionResult index()
            => View();
    }
}