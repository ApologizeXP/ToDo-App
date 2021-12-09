using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demotest.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) :base(options)
        {

        }
        public DbSet<Users> Users { get; set; }

        public DbSet<Tasks> Tasks { get; set; }
    }
}
