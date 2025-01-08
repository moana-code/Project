using GestorDeTarefasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTarefasAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais (ex.: chaves estrangeiras)
            modelBuilder.Entity<Tarefa>()
                        .HasOne(t => t.Usuario)
                        .WithMany(u => u.Tarefas)
                        .HasForeignKey(t => t.UsuarioId);
        }
    }
}
