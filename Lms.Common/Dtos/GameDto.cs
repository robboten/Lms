using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Common.Dtos
{
    /// <summary>
    /// DTO for games
    /// </summary>
    public class GameDto
    {
        /// <summary>
        /// game id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of the game
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Start date/time for the game
        /// </summary>
        public DateTime StartDate { get; set; }
    }
}
