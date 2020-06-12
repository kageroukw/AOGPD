using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AOGPD.Models;
using System.Reflection;

namespace AOGPD.Database
{
    public class AOGDbContext : DbContext
    {
        public AOGDbContext(DbContextOptions<AOGDbContext> options) : base(options)
        {

        }

        public DbSet<CivilianCharacter> Character { get; set; }
        public DbSet<CivilianLicensePlate> LicensePlate { get; set; }
        public DbSet<Dispatcher> Dispatch { get; set; }
        public DbSet<Bolo> Bolo { get; set; }
        public DbSet<PenalCode> PenalCode { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var chr = builder.Entity<CivilianCharacter>().ToTable("CivilianCharacter");
            chr.HasIndex(x => x.Id);
            chr.HasIndex(x => x.FirstName);
            chr.HasIndex(x => x.LastName);
            chr.HasIndex(x => x.DateofBirth);
            chr.HasIndex(x => x.Citations);
            chr.HasIndex(x => x.Wanted);

            var lic = builder.Entity<CivilianLicensePlate>().ToTable("CivilianLicensePlate");
            lic.HasIndex(x => x.Id);
            lic.HasIndex(x => x.LicensePlate);
            lic.HasIndex(x => x.PlateOwner);
            lic.HasIndex(x => x.Registration);
            lic.HasIndex(x => x.Insurance);
            lic.HasIndex(x => x.Additional);
            lic.HasIndex(x => x.VehicleName);
            lic.HasIndex(x => x.VehicleColor);
            lic.HasIndex(x => x.AdditionalVehicleDetails);

            var disp = builder.Entity<Dispatcher>().ToTable("Dispatcher");
            disp.HasIndex(x => x.Id);
            disp.HasIndex(x => x.UserName);
            disp.HasIndex(x => x.Password);

            var bol = builder.Entity<Bolo>().ToTable("Bolo");
            bol.HasIndex(x => x.Id);
            bol.HasIndex(x => x.LicensePlate);
            bol.HasIndex(x => x.VehicleName);
            bol.HasIndex(x => x.VehicleColor);
            bol.HasIndex(x => x.WantedFor);

            var pec = builder.Entity<PenalCode>().ToTable("PenalCode");
            pec.HasIndex(x => x.Id);
            pec.HasIndex(x => x.Code);
            pec.HasIndex(x => x.PenalTime);
        }
    }
}