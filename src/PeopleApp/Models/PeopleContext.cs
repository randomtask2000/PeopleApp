using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace PeopleApp.Models
{
    public class PeopleContext : DbContext
    {
        //Todo:        public IConfigurationRoot Configuration { get; }

        public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("People");
        }

        //todo:
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\v11.0; Initial Catalog=PeopleContext-20161017193541; Integrated Security=True; MultipleActiveResultSets=True; AttachDbFilename=|DataDirectory|PeopleContext-20161017193541.mdf");
        //}
    }
}