using System;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace forwardDojo.Models
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Joined> Joineds { get; set; }

    }
}