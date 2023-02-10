using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Common.Dtos
{
    /// <summary>
    /// Dto for creating game
    /// </summary>
    public class CreateGameDto
    {
        /// <summary>
        /// Game title
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Game start date/time
        /// </summary>
        public DateTime StartDate { get; set; }
    }
}
