using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SearchIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<KeySkills> KeySkills { get; set; }
        public DbSet<Postings> Postings { get; set; }
        public DbSet<PostKeySkills> PostKeySkills { get; set; }
    //    public DbSet<ReportPost> ReportPosts { get; set; }
     //   public DbSet<ReportUser> ReportUsers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ApplyFor> AppliedFors { get; set; }

    }
}
