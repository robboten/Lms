using AutoMapper;
using Lms.Api.Filters;
using Lms.Core.Dtos;
using Lms.Core.Models.Entities;
using Lms.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lms.Api.Controllers
{
    [ServiceFilter(typeof(ValidateModelStateAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        //private readonly LmsApiContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TournamentsController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournament([FromQuery] TournamentParameters tournamentParameters)
        {
            var tournaments = _uow.TournamentRepository.GetAll(tournamentParameters);

            var tournamentsdto = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);

            var metadata = _mapper.Map<PaginationMetaData>(tournaments);

            Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(metadata));
            return Ok(tournamentsdto);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetById(int id)
        {
            var tournament = _uow.TournamentRepository.GetById(id);

            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TournamentDto>(tournament));
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
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            _uow.TournamentRepository.CreateTournament(tournament);
            await _uow.CompleteAsync();

            return CreatedAtAction("GetTournament", new { id = tournament.Id }, _mapper.Map<TournamentDto>(tournament));
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = _uow.TournamentRepository.GetById(id);

            if (tournament == null)
            {
                return NotFound();
            }

            _uow.TournamentRepository.RemoveTournament(tournament);
            await _uow.CompleteAsync();

            return NoContent();
        }
    }
}
