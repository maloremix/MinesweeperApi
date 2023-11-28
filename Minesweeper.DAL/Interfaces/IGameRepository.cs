using Minesweeper.DAL.Interfaces;
using Minesweeper.DAL.Models;

namespace Minesweeper.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс, предоставляющий методы для работы с репозиторием игр.
    /// </summary>
    public interface IGameRepository : IBaseRepository<GameState>
    {
        /// <summary>
        /// Возвращает игру по её идентификатору.
        /// </summary>
        /// <param name="guid">Идентификатор игры.</param>
        /// <returns>Игра с указанным идентификатором.</returns>
        Task<GameState> GetByGuid(Guid guid);
    }
}
