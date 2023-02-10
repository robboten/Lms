using AutoMapper;
using Lms.Common.Dtos;
using Lms.Core.Models.Entities;
using Lms.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lms.Api.Controllers
{

    /// <summary>
    /// Api Controller for games
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for controller handling games
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="mapper"></param>
        public GamesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        // GET: api/Games
        /// <summary>
        /// Get all games
        /// </summary>
        /// <param name="GameParameters"></param>
        /// <returns>IEnumberable of games</returns>
        /// <response code="200">Returns list of games</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame([FromQuery] GameParameters GameParameters)
        {
            var Games = await _uow.GameRepository.GetAllAsync(GameParameters);

            var Gamesdto = _mapper.Map<IEnumerable<GameDto>>(Games);

            var metadata = _mapper.Map<PaginationMetaData>(Games);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(Gamesdto);
        }

        // GET: api/Games/5
        /// <summary>
        /// Get game by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Returns a game by id</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetById(int id)
        {
            var Game = await _uow.GameRepository.GetByIdAsync(id);

            if (Game == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameDto>(Game));
        }

        //// PUT: api/Games/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateGame(int id, Game Game)
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
        /// <summary>
        /// Add a new game
        /// </summary>
        /// <param name="game"></param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Returns created game</response>
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(CreateGameDto game)
        {
            var newItem = _mapper.Map<Game>(game);
            _uow.GameRepository.CreateGame(newItem);

            await _uow.CompleteAsync();

            return CreatedAtAction("GetGame", new { id = newItem.Id }, _mapper.Map<GameDto>(newItem));
        }

        // DELETE: api/Games/5
        /// <summary>
        /// Delete a game by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var Game = await _uow.GameRepository.GetByIdAsync(id);

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
