using Minesweeper.DAL.Models;

namespace Minesweeper.BLL.Interfaces
{
    /// <summary>
    /// Интерфейс, предоставляющий методы для управления игровым процессом.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Создает новую игру с указанными параметрами.
        /// </summary>
        /// <param name="width">Ширина игрового поля.</param>
        /// <param name="height">Высота игрового поля.</param>
        /// <param name="minesCount">Количество мин на поле.</param>
        /// <returns>Объект состояния новой игры.</returns>
        Task<GameState> CreateNewGame(int width, int height, int minesCount);

        /// <summary>
        /// Выполняет ход в игре с указанными параметрами.
        /// </summary>
        /// <param name="gameId">Идентификатор игры.</param>
        /// <param name="row">Номер строки на игровом поле.</param>
        /// <param name="column">Номер столбца на игровом поле.</param>
        /// <returns>Обновленный объект состояния игры после хода.</returns>
        Task<GameState> MakeMove(Guid gameId, int row, int column);
    }
}
