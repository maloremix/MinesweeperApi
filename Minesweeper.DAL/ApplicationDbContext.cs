using Microsoft.EntityFrameworkCore;
using Minesweeper.DAL.Models;
using Newtonsoft.Json;

namespace Minesweeper.DAL
{
    /// <summary>
    /// Контекст базы данных для работы с данными об игре.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="options">Опции конфигурации контекста базы данных.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Получает или устанавливает набор данных о состоянии игры.
        /// </summary>
        public DbSet<GameState> GameStates { get; set; }

        /// <summary>
        /// Настраивает преобразование для поля MinesField.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели для конфигурации.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameState>()
                .Property(e => e.MinesField)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<string[][]>(v)
                );

            modelBuilder.Entity<GameState>()
                .Property(e => e.CurrentFieldState)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<string[][]>(v)
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
