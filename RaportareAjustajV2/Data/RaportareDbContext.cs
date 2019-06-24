using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaportareAjustajV2;

namespace RaportareAjustajV2
{
    public class RaportareDbContext :DbContext
    {
        public RaportareDbContext(DbContextOptions<RaportareDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<ElindModel> ElindModels { get; set; }
        public DbSet<EltiModel> EltiModels { get; set; }
        public DbSet<FierastraieModel> FierastraieModels { get; set; }
        public DbSet<NovafluxModel> NovafluxModels { get; set; }
        public DbSet<PellatriceLandgrafModel> PellatriceLandgrafModels { get; set; }
        public DbSet<PresaValdoraModel> PresaValdoraModels { get; set; }
        public DbSet<RuillatriceLandgrafModel> RuillatriceLandgrafModels { get; set; }
        public DbSet<RullatriceProjectManModel> RullatriceProjectManModels { get; set; }
        public DbSet<GaddaModel> GaddaModels { get; set; }
    }
}
