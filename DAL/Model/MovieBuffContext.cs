using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Model
{
    public partial class MovieBuffContext : DbContext
    {
        public virtual DbSet<AppearsIn> AppearsIn { get; set; }
        public virtual DbSet<Errlog> Errlog { get; set; }
        public virtual DbSet<Mcharacter> Mcharacter { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Quote> Quote { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppearsIn>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.CharacterId });

                entity.ToTable("appears_in");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.CharacterId).HasColumnName("character_id");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AppearsIn)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_appears_in_character");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.AppearsIn)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_appears_in_movie");
            });

            modelBuilder.Entity<Errlog>(entity =>
            {
                entity.ToTable("errlog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Mcharacter>(entity =>
            {
                entity.ToTable("mcharacter");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Rating).HasColumnName("rating");
            });

            modelBuilder.Entity<Quote>(entity =>
            {
                entity.ToTable("quote");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CharacterId).HasColumnName("character_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Quote1)
                    .IsRequired()
                    .HasColumnName("quote")
                    .HasMaxLength(512);

                entity.HasOne(d => d.AppearsIn)
                    .WithMany(p => p.Quote)
                    .HasForeignKey(d => new { d.MovieId, d.CharacterId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_quote_appears_in");
            });
        }
    }
}
