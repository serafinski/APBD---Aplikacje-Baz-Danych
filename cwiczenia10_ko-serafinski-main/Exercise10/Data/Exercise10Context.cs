using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercise10.Models;

namespace Exercise10.Data
{
    public class Exercise10Context : DbContext
    {
        public Exercise10Context (DbContextOptions<Exercise10Context> options)
            : base(options)
        {
        }

        //To reprezentuje film w bazie danych
        public DbSet<Exercise10.Models.Movie> Movie { get; set; } = default!;
    }
}
