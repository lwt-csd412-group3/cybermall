using CyberMall.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMall.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ItemListing> ItemListings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationship between ItemListing and ApplicationUser
            builder.Entity<ItemListing>()
                .HasOne(il => il.Seller)
                .WithMany()
                .HasForeignKey(il => il.SellerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<CyberMall.Models.ItemSale> ItemSale { get; set; }
    }
}
