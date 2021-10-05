using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FightsApi_Data
{
    public partial class P3_NotFightClubContext : DbContext
    {
        public P3_NotFightClubContext()
        {
        }

        public P3_NotFightClubContext(DbContextOptions<P3_NotFightClubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Fight> Fights { get; set; }
        public virtual DbSet<Fighter> Fighters { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }
        public virtual DbSet<Weather> Weathers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = P3_NotFightClub; Trusted_Connection = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Fight>(entity =>
            {
                entity.ToTable("Fight");

                entity.Property(e => e.EndDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Fights)
                    .HasForeignKey(d => d.Location)
                    .HasConstraintName("FK__Fight__Location__3D5E1FD2");

                entity.HasOne(d => d.WeatherNavigation)
                    .WithMany(p => p.Fights)
                    .HasForeignKey(d => d.Weather)
                    .HasConstraintName("FK__Fight__Weather__3E52440B");
            });

            modelBuilder.Entity<Fighter>(entity =>
            {
                entity.ToTable("Fighter");

                entity.Property(e => e.IsWinner).HasColumnName("isWinner");

                entity.HasOne(d => d.Fight)
                    .WithMany(p => p.Fighters)
                    .HasForeignKey(d => d.FightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Fighter__FightId__412EB0B6");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Location1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Location");
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasIndex(e => new { e.FightId, e.UserId }, "Votes_UNQ")
                    .IsUnique();

                entity.HasOne(d => d.Fight)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.FightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Votes__FightId__45F365D3");

                entity.HasOne(d => d.Fighter)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.FighterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Votes__FighterId__46E78A0C");
            });

            modelBuilder.Entity<Weather>(entity =>
            {
                entity.ToTable("Weather");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
