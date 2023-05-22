using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class HospitalContext : DbContext
{
     public DbSet<Sensor> Sensors { get; set; }
     public DbSet<Room> Rooms { get; set; }
     public DbSet<Patient> Patients { get; set; }
     public DbSet<SensorValue> SensorValue { get; set; }
     public DbSet<Doctor> Doctors { get; set; }
     public DbSet<Admin> Admins { get; set; }
     public DbSet<Receptionist> Receptionists { get; set; }
     public DbSet<Request> Request { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Hospital.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sensor>().HasKey(sensor => sensor.Id);
        modelBuilder.Entity<Room>().HasKey(room => room.Id);
        modelBuilder.Entity<Patient>().HasKey(patient => patient.Id);
        modelBuilder.Entity<SensorValue>().HasKey(sensorValueId => sensorValueId.ValueId);
        modelBuilder.Entity<Doctor>().HasKey(doctor => doctor.Id);
        modelBuilder.Entity<Receptionist>().HasKey(receptionist => receptionist.Id);
        modelBuilder.Entity<Request>().HasKey(request => request.Id);
        modelBuilder.Entity<Admin>().HasKey(admin => admin.Id);
    }
}