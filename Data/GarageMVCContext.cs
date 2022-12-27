using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GarageMVC.Models.Entities;

namespace GarageMVC.Data
{
    public class GarageMVCContext : DbContext
    {
        public GarageMVCContext (DbContextOptions<GarageMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicle { get; set; } = default!;
    }
}
