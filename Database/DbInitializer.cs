using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOGPD.Database
{
    public static class DbInitializer
    {
        public static void Initialize(AOGDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Character.Any())
            {
                return;
            }

        }
    }
}