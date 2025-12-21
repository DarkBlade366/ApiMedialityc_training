using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiMedialityc_training.Features.Users.Models;

namespace ApiMedialityc_training.Data
{
    public class MedialitycDBContext : DbContext
    {
        public MedialitycDBContext(DbContextOptions<MedialitycDBContext> options) 
            : base(options){}


        //Add DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<UserEmail> UserEmails { get; set; }
        public DbSet<UserPhone> UserPhones { get; set; }

    }
}
