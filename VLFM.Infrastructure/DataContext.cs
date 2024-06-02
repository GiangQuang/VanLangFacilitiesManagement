using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Infrastructure
{
    public class DataContext : DbContext
    { 
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<UserDetails> Users { get; set; }
        public DbSet<EmployeeDetails> Employees { get; set; }
        public DbSet<BranchDetails> Branches { get; set; }
        public DbSet<DepartmentDetails> Departments { get; set; }
        public DbSet<PropertyTypeDetails> PropTypes { get; set; }
        public DbSet<PropertyDetails> Properties { get; set; }
        public DbSet<StatusDetails> Statuses { get; set; }
        public DbSet<ProposeDetails> Proposes { get; set; }
        public DbSet<ProviderDetails> Providers { get; set; }
        public DbSet<ReceiptDetails> Receipts { get; set; }
        public DbSet<DetailedReceiptDetails> DetailedReceipts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=MSI;Database=facilitiesdb;User Id=sa;Password=12345;Persist Security Info=True");
            }
        }
    }
}
