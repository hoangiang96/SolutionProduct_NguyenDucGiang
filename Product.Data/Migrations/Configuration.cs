namespace Product.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Product.Data.ProductDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Product.Data.ProductDbContext context)
        {
            CreateProductCategorySample(context);
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ProductDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ProductDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "HoanGiang",
            //    Email = "ducgiang12296@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Technology Education"

            //};
            //    manager.Create(user, "123654$");

            //    if (!roleManager.Roles.Any())
            //    {
            //        roleManager.Create(new IdentityRole { Name = "Admin" });
            //        roleManager.Create(new IdentityRole { Name = "User" });
            //    }

            //    var adminUser = manager.FindByEmail("ducgiang12296@gmail.com");

            //    manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
        private void CreateProductCategorySample(Product.Data.ProductDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {

                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory(){Name="Điện lạnh",Alias="Dien-Lanh",Status=true},
                    new ProductCategory(){Name="Viễn Thông",Alias="vien-thong",Status=true},
                    new ProductCategory(){Name="Đồ gia dụng",Alias="gia-dung",Status=true},
                    new ProductCategory(){Name="Mỹ Phẩm",Alias="my-pham",Status=true},
                    new ProductCategory(){Name="Điện tử",Alias="Dien-tu",Status=true},
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }
    }
}
