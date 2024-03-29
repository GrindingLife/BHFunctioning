﻿using BHFunctioning.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BHFunctioning.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HealthData> HealthData { get; set; }
        public DbSet<InputLog> InputLogs { get; set; }
        public DbSet<DensityData> DensityDatas { get; set; }

    }
}