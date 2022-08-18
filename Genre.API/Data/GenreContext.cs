using App.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genre.API.Data
{
    public class GenreContext : DbContext
    {
        public GenreContext(DbContextOptions<GenreContext> options) : base(options)
        {
        }

        public DbSet<GenreDetail> GenreDetails { get; set; }
    }
}
