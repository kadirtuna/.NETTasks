using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Shared.Models;

namespace Database.API.Models
{
    public class DataContext: DbContext
    {
        private readonly DbContextOptions<DataContext> _options;

        public DataContext(DbContextOptions<DataContext> options): base(options) {
            _options = options;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(m => m.Username).IsUnique();
        }
    }
}
