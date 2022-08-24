using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Library;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspCoreMvc_App.Models
{
    public class StudentDetailContext : IdentityDbContext<AppUser, AppRole, string>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var mysqlConnectionString = $"server=localhost;userid=root;pwd=123;" + $"port=3306;database=Student-DB";
            optionsBuilder.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString), mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure();
            });
        }
        //public StudentDetailContext(DbContextOptions<StudentDetailContext> options) : base(options)
        //{
        //}
        //public DbSet<StudentDetail> StudentDetails { get; set; }
        //public DbSet<UserData> UserDatas { get; set; }
        //Table name StudentDetails
    }
}
