using AutoMapper;
using Lms.Core.Dtos;
using Lms.Core.Models.Entities;
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
            CreateMap<Tournament, TournamentDto>();
            CreateMap<PagedList<Tournament>, PaginationMetaData>();
            CreateMap<PagedList<Game>, PaginationMetaData>();
        }
    }
}
