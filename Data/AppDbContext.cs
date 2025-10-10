using Microsoft.EntityFrameworkCore;
using SkyTravel.Models;

namespace SkyTravel.Data;

public class AppDbContext : DbContext
{
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Fly> Flights { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Models Building
        modelBuilder.Entity<Status>()
            .HasKey(s => s.Id);
        
        modelBuilder.Entity<Passenger>()
            .HasKey(p => p.Id);
        
        modelBuilder.Entity<Fly>()
            .HasKey(f => f.Id);
            //.Property(f => f.Code)
            //.HasDefaultValue(metodoNombre()); esto lo puedo usar para generar el valor
            
        modelBuilder.Entity<Reservation>()
            .HasKey(r => r.Id);
            //.Property(f => f.Code)
            //.HasDefaultValue(metodoNombre());
        
        //Relation-ship for Fly and Status
        modelBuilder.Entity<Fly>()
            .HasOne(f => f.Status)
            .WithMany(s => s.Flights)
            .HasForeignKey(f => f.StatusId)
            .HasPrincipalKey(s => s.Id);

        //RelationShips with Reservation
        modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Status)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.StatusId)
                .HasPrincipalKey(s => s.Id);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Fly)
            .WithMany(f => f.Reservations)
            .HasForeignKey(r => r.FlyId)
            .HasPrincipalKey(f => f.Id);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Passenger)
            .WithMany(p => p.Reservations)
            .HasForeignKey(r => r.PassengerId)
            .HasPrincipalKey(p => p.Id);
    }
}