//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FileStorage.Web.Identity
//{
//    public class IdentityContext : IdentityDbContext
//    {
//        public IdentityContext(DbContextOptions options) : base(options)
//        {
//            Database.EnsureCreated();
//            if (!Roles.Any())
//            {
//                Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole() { Name = "User", NormalizedName = "USER" });
//                Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });
//                Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole() { Name = "Guest", NormalizedName = "GUEST" });
//                SaveChanges();
//            }
//        }
//    }
//}
