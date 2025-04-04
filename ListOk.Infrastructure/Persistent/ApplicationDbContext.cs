using ListOk.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ListOk.Infrastructure.Persistent
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка первичных ключей
            modelBuilder.Entity<Board>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Column>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Card>()
                .HasKey(c => c.Id);

            // Настройка отношений между сущностями

            // Board -> Columns (один ко многим)
            modelBuilder.Entity<Board>()
                .HasMany(b => b.Columns)
                .WithOne(c => c.Board)
                .HasForeignKey(c => c.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Column -> Cards (один ко многим)
            modelBuilder.Entity<Column>()
                .HasMany(c => c.Cards)
                .WithOne(card => card.Column)
                .HasForeignKey(card => card.ColumnId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

