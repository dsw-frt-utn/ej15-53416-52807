using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data.EF;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Speciality> Specialities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ─── BaseEntity ───
        modelBuilder.Entity<Doctor>().HasKey(d => d.Id);
        modelBuilder.Entity<Speciality>().HasKey(s => s.Id);

        // ─── Doctor ───
        modelBuilder.Entity<Doctor>()
            .Property(d => d.Name)
            .IsRequired();

        modelBuilder.Entity<Doctor>()
            .Property(d => d.LicenseNumber)
            .IsRequired();

        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Speciality)
            .WithMany()
            .IsRequired();

        // ─── Speciality ───
        modelBuilder.Entity<Speciality>()
            .Property(s => s.Name)
            .IsRequired();

        modelBuilder.Entity<Speciality>()
            .Property(s => s.Description)
            .IsRequired();
    }
}
