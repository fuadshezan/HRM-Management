using GTHRM.Models;
using Microsoft.EntityFrameworkCore;

namespace GTHRM.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceSummary> AttendanceSummaries { get; set; }
        public DbSet<Salary> salaries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Triggers
            modelBuilder.Entity<Employee>()
                     .ToTable(tb => tb.HasTrigger("calculateBasic"));

            modelBuilder.Entity<Department>()
                .HasIndex(d => new { d.ComId, d.DepartmentName })
                .IsUnique();

            modelBuilder.Entity<Designation>()
                .HasIndex(d => new { d.ComId, d.DesigName })
                .IsUnique();
            modelBuilder.Entity<Employee>()
                .HasIndex(d => new { d.ComId, d.EmpCode })
                .IsUnique();

            modelBuilder.Entity<Attendance>()
                .HasKey(d => new { d.ComId, d.EmpId, d.dtDate });

            modelBuilder.Entity<AttendanceSummary>()
                .HasKey(d => new { d.ComId, d.EmpId, d.year,d.month });

            modelBuilder.Entity<Salary>()
                .HasKey(d => new { d.ComId, d.EmpId, d.year, d.month });

            modelBuilder.Entity<Company>()
                .Property(c => c.ComId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Department>()
                .Property(d => d.DeptId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Designation>()
                .Property(d => d.DesigId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Employee>()
                .Property(d => d.EmpId)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Shift>()
                .Property(d => d.ShiftId)
                .HasDefaultValueSql("NEWID()");


        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {

        }

    }
}
