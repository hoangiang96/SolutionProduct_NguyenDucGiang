﻿using Microsoft.AspNet.Identity.EntityFramework;
using Product.Model.Models;
using System.Data.Entity;

namespace Product.Data
{
    public class ProductDbContext : IdentityDbContext<ApplicationUser>
    {
        public ProductDbContext() : base("ProductConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Footer> Footers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Item> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<SupportOnline> SupportOnlines { get; set; }

        public DbSet<SystemConfig> SystemConfigs { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<VisitorStatistic> VisitorStatistics { get; set; }
        public DbSet<Error> Errors { get; set; }

        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        //public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        //public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public static ProductDbContext Create()
        {
            return new ProductDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);
        }
    }
}