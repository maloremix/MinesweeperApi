using System.Text.Json.Serialization;

namespace Minesweeper.Dto
{
    /// <summary>
    /// Класс, представляющий объект ответа с информацией о текущем состоянии игры.
    /// </summary>
    public class GameInfoResponse
    {
        /// <summary>
        /// Уникальный идентификатор игры.
        /// </summary>
        [JsonPropertyName("game_id")]
        public Guid GameId { get; set; }

        /// <summary>
        /// Ширина игрового поля.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота игрового поля.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Количество мин на поле.
        /// </summary>
        [JsonPropertyName("mines_count")]
        public int MinesCount { get; set; }

        /// <summary>
        /// Флаг, указывающий, завершена ли игра.
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Двумерный массив, представляющий текущее состояние игрового поля.
        /// </summary>
        public string[][] Field { get; set; }
    }
}
