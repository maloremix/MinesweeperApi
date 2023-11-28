using Microsoft.EntityFrameworkCore;
using Minesweeper.DAL.Interfaces;
using Minesweeper.DAL.Models;

namespace Minesweeper.DAL.Repositories
{
    /// <summary>
    /// Реализация репозитория для взаимодействия с состоянием игры в базе данных.
    /// </summary>
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GameRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создает новую запись о состоянии игры в базе данных.
        /// </summary>
        /// <param name="gameState">Объект состояния игры.</param>
        /// <returns>Успешно ли создана запись.</returns>
        public async Task<bool> Create(GameState gameState)
        {
            _context.GameStates.Add(gameState);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Получает состояние игры по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор состояния игры.</param>
        /// <returns>Объект состояния игры.</returns>
        public Task<GameState> Get(int id)
        {
            return _context.GameStates.FirstOrDefaultAsync(g => g.ID == id);
        }

        /// <summary>
        /// Получает состояние игры по уникальному идентификатору игры.
        /// </summary>
        /// <param name="guid">Уникальный идентификатор игры.</param>
        /// <returns>Объект состояния игры.</returns>
        public async Task<GameState> GetByGuid(Guid guid)
        {
            return await _context.GameStates.FirstOrDefaultAsync(g => g.GameGuid == guid);
        }

        /// <summary>
        /// Получает список всех состояний игр из базы данных.
        /// </summary>
        /// <returns>Список состояний игр.</returns>
        public Task<List<GameState>> Select()
        {
            return _context.GameStates.ToListAsync();
        }

        /// <summary>
        /// Удаляет запись о состоянии игры из базы данных.
        /// </summary>
        /// <param name="gameState">Объект состояния игры.</param>
        /// <returns>Успешно ли удалена запись.</returns>
        public async Task<bool> Delete(GameState gameState)
        {
            _context.GameStates.Remove(gameState);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Обновляет запись о состоянии игры в базе данных.
        /// </summary>
        /// <param name="gameState">Объект состояния игры.</param>
        /// <returns>Обновленный объект состояния игры.</returns>
        public async Task<GameState> Update(GameState gameState)
        {
            _context.GameStates.Update(gameState);
            await _context.SaveChangesAsync();
            return gameState;
        }
    }
}
