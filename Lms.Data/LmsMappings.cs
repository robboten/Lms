using AutoMapper;
using Lms.Common.Dtos;
using Lms.Core.Models.Entities;
using Lms.Core.Models.Entities.Helpers;

namespace Lms.Data
{
    public class LmsMappings : Profile
    {
        public LmsMappings()
        {
            CreateMap<Game, GameDto>();
            CreateMap<Tournament, TournamentDto>().ReverseMap();
            CreateMap<CreateTournamentDto, Tournament>().ReverseMap();
            CreateMap<CreateGameDto, Game>().ReverseMap();
            CreateMap<PagedList<Tournament>, PaginationMetaData>();
            CreateMap<PagedList<Game>, PaginationMetaData>();
        }
    }
}
