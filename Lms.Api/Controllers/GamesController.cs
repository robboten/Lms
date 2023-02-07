using AutoMapper;
using Lms.Core.Dtos;
using Lms.Core.Models.Entities;
using Lms.Core.Repositories;
using Lms.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GamesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame([FromQuery] GameParameters GameParameters)
        {
            var Games = _uow.GameRepository.GetAll(GameParameters);

            var Gamesdto = _mapper.Map<IEnumerable<GameDto>>(Games);

            var metadata = _mapper.Map<PaginationMetaData>(Games);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(Gamesdto);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetById(int id)
        {
            var Game = _uow.GameRepository.GetById(id);

            if (Game == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameDto>(Game));
        }

        //// PUT: api/Games/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGame(int id, Game Game)
        //{
        //    if (id != Game.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(Game).State = EntityState.Modified;

        //    try
        //    {
        //        await _uow.CompleteAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GameExists(id))
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

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game Game)
        {
            _uow.GameRepository.CreateGame(Game);
            await _uow.CompleteAsync();

            return CreatedAtAction("GetGame", new { id = Game.Id }, _mapper.Map<GameDto>(Game));
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var Game = _uow.GameRepository.GetById(id);

            if (Game == null)
            {
                return NotFound();
            }

            _uow.GameRepository.RemoveGame(Game);
            await _uow.CompleteAsync();

            return NoContent();
        }
    }
}
