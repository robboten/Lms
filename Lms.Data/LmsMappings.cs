using AutoMapper;
using Lms.Common.Dtos;
using Lms.Common.Entities;
using Lms.Common.Helpers;
using Lms.Core.Models.Entities;
using Lms.Core.Models.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
