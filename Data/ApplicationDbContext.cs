﻿using BasicPaymentGateway.Entity;
using Microsoft.EntityFrameworkCore;

namespace BasicPaymentGateway.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
                
        }
       public DbSet<Transaction> Transactions { get; set; }
    }
}
