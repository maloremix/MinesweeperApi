using AutoMapper;
using Minesweeper.DAL.Models;
using Minesweeper.Dto;

/// <summary>
/// Профиль маппинга для Minesweeper.
/// </summary>
public class MinesweeperMappingProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="MinesweeperMappingProfile"/>.
    /// </summary>
    public MinesweeperMappingProfile()
    {
        CreateMap<GameState, GameInfoResponse>()
            .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameGuid))
            .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width))
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
            .ForMember(dest => dest.MinesCount, opt => opt.MapFrom(src => src.MinesCount))
            .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed))
            .ForMember(dest => dest.Field, opt => opt.MapFrom(src => src.CurrentFieldState));
    }
}
