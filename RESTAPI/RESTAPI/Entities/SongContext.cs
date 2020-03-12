//Cameron Low
//Distributed Applications
//Assignment 2
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI.Entities
{
    public class SongContext : DbContext
    {
        public SongContext(DbContextOptions<SongContext> options) : base(options) { }

        public DbSet<Song> Songs { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Account> AccountSessions { get; set; }
    }
}
