using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Model
{
    public partial class MovieBuffContext : DbContext
    {
        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<Errlog> Errlog { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Quote> Quote { get; set; }

        // Unable to generate entity type for table 'dbo.appears_in'. Please see the warning messages.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("character");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);
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

                entity.HasIndex(e => e.CharacterId)
                    .HasName("IX_character");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CharacterId).HasColumnName("character_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Quote1)
                    .IsRequired()
                    .HasColumnName("quote")
                    .HasMaxLength(512);

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.Quote)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_character_quote");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Quote)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_quote");
            });
        }
    }
}
