using BackendSolution.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendSolution.Infrastructure.Data
{
    public class BackendContext: DbContext
    {
        public BackendContext(DbContextOptions<BackendContext> opt): base(opt) 
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
