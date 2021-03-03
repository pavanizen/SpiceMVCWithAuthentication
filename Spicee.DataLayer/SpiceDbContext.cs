using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spicee.DomainModels;

namespace Spicee.DataLayer
{
    public class SpiceDbContext:DbContext
    {
        public SpiceDbContext():base("conn")
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }

        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        //public DbSet<ApplicationUser> ApplicationUser { get; set; }






        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

    }
}
