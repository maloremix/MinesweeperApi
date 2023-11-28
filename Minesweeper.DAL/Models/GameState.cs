namespace Minesweeper.DAL.Models
{
    /// <summary>
    /// Представляет состояние игры в "Сапер".
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Уникальный идентификатор состояния игры.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Уникальный идентификатор игры.
        /// </summary>
        public Guid GameGuid { get; set; }

        /// <summary>
        /// Ширина игрового поля.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота игрового поля.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Количество мин на игровом поле.
        /// </summary>
        public int MinesCount { get; set; }

        /// <summary>
        /// Игровое поле с расположением мин.
        /// </summary>
        public string[][] MinesField { get; set; }

        /// <summary>
        /// Текущее состояние игрового поля.
        /// </summary>
        public string[][] CurrentFieldState { get; set; }

        /// <summary>
        /// Признак завершения игры.
        /// </summary>
        public bool Completed { get; set; }
    }
}
