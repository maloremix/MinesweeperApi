using System.Text.Json.Serialization;

namespace Minesweeper.Dto
{
    /// <summary>
    /// Класс, представляющий объект запроса на совершение хода в игре.
    /// </summary>
    public class GameTurnRequest
    {
        /// <summary>
        /// Уникальный идентификатор игры.
        /// </summary>
        [JsonPropertyName("game_id")]
        public Guid GameId { get; set; }

        /// <summary>
        /// Номер строки на игровом поле.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Номер столбца на игровом поле.
        /// </summary>
        public int Col { get; set; }
    }
}
