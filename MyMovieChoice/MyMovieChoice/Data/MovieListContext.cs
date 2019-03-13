using Microsoft.EntityFrameworkCore;
using MyMovieChoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieChoice.Data
{
    public class MovieListContext : DbContext
    {
        public MovieListContext(DbContextOptions<MovieListContext> options) : base(options)
        {

        }
        public DbSet<MovieList> MovieLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieList>().ToTable(nameof(MovieList));
        }
    }
}
