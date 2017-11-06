using Microsoft.EntityFrameworkCore;
using CS586MVC.Models;

namespace CS586MVC.Data
{
    public partial class PropertyMismanagementContext : DbContext
    {
        public virtual DbSet<AptComplex> AptComplexes { get; set; }
        public virtual DbSet<AptComplexUnit> AptComplexUnits { get; set; }
        public virtual DbSet<Lease> Leases { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<AptUnit> AptUnits { get; set; }

        public PropertyMismanagementContext(DbContextOptions<PropertyMismanagementContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AptComplex>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("AptComplex_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Size).HasColumnName("Size");
            });

            modelBuilder.Entity<AptUnit>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });
            
            modelBuilder.Entity<AptComplexUnit>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("AptComplexUnit_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AptComplexId).HasColumnName("AptComplexID");

                entity.Property(e => e.AptUnitId).HasColumnName("AptUnitID");

                entity.Property(e => e.UnitNumber).HasColumnName("UnitNumber");

                entity.HasOne(d => d.AptComplex)
                    .WithMany(p => p.Units)
                    .HasForeignKey(d => d.AptComplexId)
                    .HasConstraintName("AptComplexUnit_AptComplex_ID_fk");

                entity.HasOne(d => d.AptUnit)
                    .WithMany(p => p.AptComplexUnit)
                    .HasForeignKey(d => d.AptUnitId)
                    .HasConstraintName("AptComplexUnit_AptUnit_ID_fk");
            });

            modelBuilder.Entity<Lease>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Lease_ID_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AptComplexUnitId).HasColumnName("AptComplexUnitID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Lease)
                    .HasForeignKey(d => d.AptComplexUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Lease_AptComplexUnit_ID_fk");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Leases)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
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
