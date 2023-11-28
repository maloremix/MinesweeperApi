using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.BLL.Interfaces;
using Minesweeper.DAL.Models;
using Minesweeper.Dto;

/// <summary>
/// Контроллер для управления игрой "Сапёр".
/// </summary>
[ApiController]
[Route("api")]
public class MinesweeperController : ControllerBase
{
    /// <summary>
    /// Сервис для управления игрой "Сапёр".
    /// </summary>
    private readonly IGameService _gameService;

    /// <summary>
    /// Объект для маппинга между объектами разных типов.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера MinesweeperController.
    /// </summary>
    /// <param name="gameService">Сервис для работы с игрой "Сапёр".</param>
    /// <param name="mapper">Mapper для преобразования объектов.</param>
    public MinesweeperController(IGameService gameService, IMapper mapper)
    {
        _gameService = gameService;
        _mapper = mapper;
    }

    /// <summary>
    /// Создает новую игру "Сапёр" на основе предоставленных параметров.
    /// </summary>
    /// <param name="settings">Параметры для новой игры.</param>
    /// <returns>Возвращает информацию о вновь созданной игре.</returns>
    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> StartNewGame([FromBody] NewGameRequest settings)
    {
        try
        {
            GameState game = await _gameService.CreateNewGame(settings.Width, settings.Height, settings.MinesCount);

            var gameInfo = _mapper.Map<GameInfoResponse>(game);

            return Ok(gameInfo);
        }
        catch
        {
            return BadRequest(new ErrorResponse { Error = "Произошла непредвиденная ошибка" });
        }
    }

    /// <summary>
    /// Выполняет ход в существующей игре "Сапёр" на основе предоставленного запроса на ход.
    /// </summary>
    /// <param name="turnRequest">Запрос на ход с указанием ID игры, строки и столбца.</param>
    /// <returns>Возвращает обновленную информацию об игре после хода.</returns>
    [HttpPost]
    [Route("turn")]
    public async Task<IActionResult> MakeMove([FromBody] GameTurnRequest turnRequest)
    {
        try
        {
            GameState updatedState = await _gameService.MakeMove(turnRequest.GameId, turnRequest.Row, turnRequest.Col);

            var updatedGameInfo = _mapper.Map<GameInfoResponse>(updatedState);

            return Ok(updatedGameInfo);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ErrorResponse { Error = ex.Message });
        }
        catch
        {
            return BadRequest(new ErrorResponse { Error = "Произошла непредвиденная ошибка" });
        }
    }
}
