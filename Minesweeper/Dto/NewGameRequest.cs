using System.Text.Json.Serialization;

namespace Minesweeper.Dto
{
    /// <summary>
    /// Класс, представляющий объект запроса на создание новой игры.
    /// </summary>
    public class NewGameRequest
    {
        /// <summary>
        /// Количество мин на игровом поле.
        /// </summary>
        [JsonPropertyName("mines_count")]
        public int MinesCount { get; set; }

        /// <summary>
        /// Ширина игрового поля.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота игрового поля.
        /// </summary>
        public int Height { get; set; }
    }
}
