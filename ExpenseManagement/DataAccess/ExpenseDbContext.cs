﻿using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext() : base() { }
        public ExpenseDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> User { get; set; }
        public DbSet<Tickets> Ticket { get; set; }

    }
}
