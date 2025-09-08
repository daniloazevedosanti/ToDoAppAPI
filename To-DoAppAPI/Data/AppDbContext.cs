using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TodoApp.API.Models;

namespace TodoApp.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(t => t.Descricao).HasMaxLength(1000);
                entity.Property(t => t.DataCriacao).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(t => t.Concluida).HasDefaultValue(false);
            });
        }
    }
}