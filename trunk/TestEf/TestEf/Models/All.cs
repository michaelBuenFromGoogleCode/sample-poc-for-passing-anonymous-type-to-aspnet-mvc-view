using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace TestEf.Models
{

    #region Entities

    public class Person
    {
        public long PersonId { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public int FavoriteNumber { get; set; }
    }

    public class Qualified
    {
        public int QualifiedId { get; set; }
        public long PersonId { get; set; }
    }


    public class EntContext : DbContext
    {        
        public DbSet<Person> Persons { get; set; }
        public DbSet<Qualified> Qualifieds { get; set; }

        protected override void OnModelCreating(System.Data.Entity.ModelConfiguration.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Qualified>().ToTable("Qualified");
        }
    }

    #endregion


    #region Buddy classes here

    public class QualifiedStatus
    {
        public Person Person { get; set; }
        public bool IsQualified { get; set; }
    }

    #endregion

}