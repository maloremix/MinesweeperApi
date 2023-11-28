using Minesweeper.BLL.Interfaces;
using Minesweeper.DAL.Interfaces;
using Minesweeper.DAL.Models;

/// <summary>
/// Реализация сервиса для управления игровым процессом.
/// </summary>
public class GameService : IGameService
{
    /// <summary>
    /// Репозиторий игр, используемый для взаимодействия с хранилищем состояний игр.
    /// </summary>
    private readonly IGameRepository _gameRepository;

    /// <summary>
    /// Конструктор класса <see cref="GameService"/>.
    /// </summary>
    /// <param name="gameRepository">Репозиторий игры.</param>
    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    /// <summary>
    /// Создает новую игру с указанными параметрами.
    /// </summary>
    /// <param name="width">Ширина игрового поля.</param>
    /// <param name="height">Высота игрового поля.</param>
    /// <param name="minesCount">Количество мин на поле.</param>
    /// <returns>Объект состояния новой игры.</returns>
    public async Task<GameState> CreateNewGame(int width, int height, int minesCount)
    {
        var game = new GameState
        {
            GameGuid = Guid.NewGuid(),
            Width = width,
            Height = height,
            MinesCount = minesCount,
            MinesField = GenerateMinesField(width, height, minesCount),
            CurrentFieldState = InitializeEmptyField(width, height),
            Completed = false
        };

        await _gameRepository.Create(game);

        return game;
    }

    /// <summary>
    /// Выполняет ход в игре с указанными параметрами.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <param name="row">Номер строки на игровом поле.</param>
    /// <param name="column">Номер столбца на игровом поле.</param>
    /// <returns>Обновленный объект состояния игры после хода.</returns>
    public async Task<GameState> MakeMove(Guid gameId, int row, int column)
    {
        var game = await _gameRepository.GetByGuid(gameId);

        if (game == null)
        {
            return null;
        }

        OpenCell(game, row, column);

        CheckGameCompletion(game);

        await _gameRepository.Update(game);

        return game;
    }

    /// <summary>
    /// Открывает ячейку в игре.
    /// </summary>
    /// <param name="game">Объект состояния игры.</param>
    /// <param name="row">Номер строки на игровом поле.</param>
    /// <param name="col">Номер столбца на игровом поле.</param>
    private void OpenCell(GameState game, int row, int col)
    {
        if (game.Completed)
        {
            throw new InvalidOperationException("Игра завершена");
        }

        string currentCell = game.CurrentFieldState[row][col];

        if (currentCell != " ")
        {
            throw new InvalidOperationException("Уже открытая ячейка");
        }

        string mineCell = game.MinesField[row][col];

        if (mineCell == "X")
        {
            game.Completed = true;
            game.CurrentFieldState = game.MinesField;
        }
        else
        {
            game.CurrentFieldState[row][col] = mineCell;

            if (mineCell == "0")
            {
                OpenAdjacentCells(game, row, col);
            }
        }
    }


    /// <summary>
    /// Открывает смежные ячейки, если текущая ячейка не содержит мину.
    /// </summary>
    /// <param name="game">Объект состояния игры.</param>
    /// <param name="row">Номер строки на игровом поле.</param>
    /// <param name="col">Номер столбца на игровом поле.</param>
    private void OpenAdjacentCells(GameState game, int row, int col)
    {
        int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };

        for (int i = 0; i < 8; i++)
        {
            int newRow = row + dr[i];
            int newCol = col + dc[i];

            if (newRow >= 0 && newRow < game.Height && newCol >= 0 && newCol < game.Width && game.CurrentFieldState[newRow][newCol] == " ")
            {
                OpenCell(game, newRow, newCol);
                if (game.MinesField[newRow][newCol] == "0")
                {
                    OpenAdjacentCells(game, newRow, newCol);
                }
            }
        }
    }

    /// <summary>
    /// Подсчитывает количество мин в смежных ячейках.
    /// </summary>
    /// <param name="minesField">Игровое поле с минами.</param>
    /// <param name="row">Номер строки на игровом поле.</param>
    /// <param name="col">Номер столбца на игровом поле.</param>
    /// <returns>Количество мин в смежных ячейках.</returns>
    private int CountAdjacentMines(string[][] minesField, int row, int col)
    {

        int count = 0;
        int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };

        for (int i = 0; i < 8; i++)
        {
            int newRow = row + dr[i];
            int newCol = col + dc[i];

            if (newRow >= 0 && newRow < minesField.Length && newCol >= 0 && newCol < minesField[0].Length && minesField[newRow][newCol] == "X")
            {
                count++;
            }
        }

        return count;
    }

    /// <summary>
    /// Генерирует игровое поле с минами.
    /// </summary>
    /// <param name="width">Ширина игрового поля.</param>
    /// <param name="height">Высота игрового поля.</param>
    /// <param name="minesCount">Количество мин на поле.</param>
    /// <returns>Игровое поле с минами.</returns>
    private string[][] GenerateMinesField(int width, int height, int minesCount)
    {
        string[][] field = InitializeEmptyField(width, height);

        Random random = new Random();
        for (int k = 0; k < minesCount; k++)
        {
            int mineRow, mineCol;
            do
            {
                mineRow = random.Next(0, field.Length);
                mineCol = random.Next(0, field[0].Length);
            } while (field[mineRow][mineCol] == "X");

            field[mineRow][mineCol] = "X";
        }

        for (int i = 0; i < field.Length; i++)
        {
            for (int j = 0; j < field[0].Length; j++)
            {
                if (field[i][j] != "X")
                {
                    int count = CountAdjacentMines(field, i, j);
                    field[i][j] = count.ToString();
                }
            }
        }

        return field;
    }

    /// <summary>
    /// Проверяет условие завершения игры и, если оно выполнено, завершает игру.
    /// </summary>
    /// <param name="game">Объект состояния игры.</param>
    private void CheckGameCompletion(GameState game)
    {
        if (!game.Completed)
        {
            int unopenedCellCount = game.CurrentFieldState.Sum(row => row.Count(cell => cell == " "));

            if (unopenedCellCount == game.MinesCount)
            {
                CompleteGame(game);
            }
        }
    }

    /// <summary>
    /// Завершает игру, отмечая все неоткрытые ячейки как минированные.
    /// </summary>
    /// <param name="game">Объект состояния игры.</param>
    private void CompleteGame(GameState game)
    {
        game.Completed = true;

        game.CurrentFieldState = game.CurrentFieldState
            .Select(rowCells => rowCells.Select(cell => cell == " " ? "M" : cell).ToArray())
            .ToArray();
    }

    /// <summary>
    /// Инициализирует пустое игровое поле заданных размеров.
    /// </summary>
    /// <param name="width">Ширина игрового поля.</param>
    /// <param name="height">Высота игрового поля.</param>
    /// <returns>Пустое игровое поле.</returns>
    private string[][] InitializeEmptyField(int width, int height)
    {
        string[][] field = new string[height][];
        for (int i = 0; i < height; i++)
        {
            field[i] = new string[width];
            for (int j = 0; j < width; j++)
            {
                field[i][j] = " ";
            }
        }
        return field;
    }
}
