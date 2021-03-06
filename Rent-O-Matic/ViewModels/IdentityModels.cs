﻿using Microsoft.AspNet.Identity.EntityFramework;
using Rent_O_Matic.Models;
using System.Data.Entity;

namespace Rent_O_Matic.ViewModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RentalsHistory> RentalsHistories { get; set; }
        public DbSet<IncidentGravity> IncidentGravities { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}