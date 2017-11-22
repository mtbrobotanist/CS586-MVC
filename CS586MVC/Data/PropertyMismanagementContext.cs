using Microsoft.EntityFrameworkCore;
using CS586MVC.Models;

namespace CS586MVC.Data
{
    public partial class PropertyMismanagementContext : DbContext
    {
        public virtual DbSet<ApartmentComplex> ApartmentComplexes { get; set; }
        public virtual DbSet<ApartmentComplexUnit> ApartmentComplexUnits { get; set; }
        public virtual DbSet<Lease> Leases { get; set; }
        public virtual DbSet<Person> People { get; set; }
        //public virtual DbSet<ApartmentUnit> ApartmentUnits { get; set; }

        public PropertyMismanagementContext(DbContextOptions<PropertyMismanagementContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApartmentComplex>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("ApartmentComplex_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasIndex(e => e.Address)
                    .HasName("AparmentComplex_Address_uindex")
                    .IsUnique();
                
                entity.Property(e => e.Size).HasColumnName("Size");
            });

//            modelBuilder.Entity<ApartmentUnit>(entity =>
//            {
//                entity.Property(e => e.Id).HasColumnName("ID");
//            });
            
            modelBuilder.Entity<ApartmentComplexUnit>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("ApartmentComplexUnit_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApartmentComplexId).HasColumnName("ApartmentComplexID");

                //entity.Property(e => e.ApartmentUnitId).HasColumnName("ApartmentUnitID");

                entity.Property(e => e.UnitNumber).HasColumnName("UnitNumber");

                entity.HasOne(d => d.ApartmentComplex)
                    .WithMany(p => p.ApartmentComplexUnits)
                    .HasForeignKey(d => d.ApartmentComplexId)
                    .HasConstraintName("ApartmentComplexUnit_ApartmentComplex_ID_fk")
                    .OnDelete(DeleteBehavior.Cascade);

//                entity.HasOne(d => d.ApartmentUnit)
//                    .WithMany(p => p.ApartmentComplexUnit)
//                    .HasForeignKey(d => d.ApartmentUnitId)
//                    .HasConstraintName("ApartmentComplexUnit_ApartmentUnit_ID_fk")
//                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Lease>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Lease_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApartmentComplexUnitId).HasColumnName("ApartmentComplexUnitID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.ApartmentComplexUnit)
                    .WithMany(p => p.Leases)
                    .HasForeignKey(d => d.ApartmentComplexUnitId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Lease_ApartmentComplexUnit_ID_fk");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Leases)
                    .HasForeignKey(d => d.PersonId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Lease_Person_ID_fk");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Person_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
