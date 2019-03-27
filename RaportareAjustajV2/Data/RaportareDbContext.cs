using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public class RaportareDbContext :DbContext
    {
        public RaportareDbContext(DbContextOptions<RaportareDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

    }
}
