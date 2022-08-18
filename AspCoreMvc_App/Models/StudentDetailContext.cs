using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspCoreMvc_App.Models
{
    public class StudentDetailContext : DbContext
    {

        public StudentDetailContext(DbContextOptions<StudentDetailContext> options) : base(options)
        {
        }
        public DbSet<StudentDetail> StudentDetails { get; set; }
        public DbSet<UserData> UserDatas { get; set; }
        //Table name StudentDetails
    }
}
