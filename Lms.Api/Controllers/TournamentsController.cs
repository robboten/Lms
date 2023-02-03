using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Core.Models.Entities;
using Lms.Data;
using Lms.Data.Context;
using CodeEvents.Api.Core.Repositories;
using Lms.Data.Repositories;
using Lms.Core.Repositories;
using AutoMapper;
using Lms.Core.Dtos;
using Lms.Api.Filters;

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
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournament([FromQuery] ApiParameters apiParameters)
        {
            if (_uow.TournamentRepository == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<TournamentDto>>(await _uow.TournamentRepository.GetAllAsync(apiParameters)));
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _uow.TournamentRepository.GetAsync(id);

            //move to filter?
            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map < TournamentDto> (tournament));
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
            if (_uow.TournamentRepository == null)
            {
                return NotFound();
            }

            _uow.TournamentRepository.Add(tournament);
            await _uow.CompleteAsync();

            return CreatedAtAction("GetTournament", new { id = tournament.Id }, _mapper.Map<TournamentDto>(tournament));
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            if (_uow.TournamentRepository == null)
            {
                return NotFound();
            }
            var tournament = await _uow.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _uow.TournamentRepository.Remove(tournament);
            await _uow.CompleteAsync();

            return NoContent();
        }
    }
}
