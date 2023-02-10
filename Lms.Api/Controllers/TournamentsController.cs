using AutoMapper;
using Lms.Api.Filters;
using Lms.Common.Dtos;
using Lms.Core.Models.Entities;
using Lms.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lms.Api.Controllers
{
    [ServiceFilter(typeof(ValidateModelStateAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [Produces("application/json")]
    public class TournamentsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TournamentsController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournament([FromQuery] TournamentParameters tournamentParameters)
        {
            var tournaments = await _uow.TournamentRepository.GetAllAsync(tournamentParameters);

            var metadata = _mapper.Map<PaginationMetaData>(tournaments);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(_mapper.Map<IEnumerable<TournamentDto>>(tournaments));
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetById(int id, [FromQuery] TournamentParameters tournamentParameters)
        {
            var tournament = await _uow.TournamentRepository.GetByIdAsync(id, tournamentParameters);

            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TournamentDto>(tournament));
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {

            var tournament = await _uow.TournamentRepository.GetByIdAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            _uow.TournamentRepository.RemoveTournament(tournament);
            await _uow.CompleteAsync();

            return NoContent();
        }

        //// PUT: api/Tournaments/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        //{
        //    if (id != tournament.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tournament).State = EntityState.Modified;

        //    try
        //    {
        //        await _uow.CompleteAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TournamentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> PostTournament(CreateTournamentDto dto)
        {
            var newItem = _mapper.Map<Tournament>(dto);

            await _uow.TournamentRepository.CreateTournament(newItem);
            await _uow.CompleteAsync();

            return CreatedAtAction(nameof(GetTournament), new { id = newItem.Id }, _mapper.Map<TournamentDto>(newItem));
        }


    }
}
