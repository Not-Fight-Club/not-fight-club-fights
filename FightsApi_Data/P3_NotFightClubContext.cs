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
     
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // if (!optionsBuilder.IsConfigured)
            {

            }
        }
        */

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
                    .HasConstraintName("FK_Fight_Location");

                entity.HasOne(d => d.WeatherNavigation)
                    .WithMany(p => p.Fights)
                    .HasForeignKey(d => d.Weather)
                    .HasConstraintName("FK_Fight_Weather");
            });

            modelBuilder.Entity<Fighter>(entity =>
            {
                entity.ToTable("Fighter");

                entity.Property(e => e.IsWinner).HasColumnName("isWinner");

                entity.HasOne(d => d.Fight)
                    .WithMany(p => p.Fighters)
                    .HasForeignKey(d => d.FightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fighter_Fight");
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
                    .HasConstraintName("FK_Votes_Fight");

                entity.HasOne(d => d.Fighter)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.FighterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Votes_Fighter");
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
