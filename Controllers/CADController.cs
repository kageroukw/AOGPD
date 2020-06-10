using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOGPD.Database;
using Microsoft.AspNetCore.Mvc;

namespace AOGPD.Controllers
{
    public class CADController : Controller
    {
        private readonly AOGDbContext _context;

        public CADController(AOGDbContext context)
        {
            _context = context;
        }


    }
}