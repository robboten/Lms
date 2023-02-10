using System.ComponentModel.DataAnnotations;

namespace Lms.Common.Dtos
{
    /// <summary>
    /// Dto for creating Tournaments
    /// </summary>
    public class CreateTournamentDto
    {
        /// <summary>
        /// Tournament title
        /// </summary>
        [Required(ErrorMessage ="Event name required")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Tournament start date
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// List of games during the tournament
        /// </summary>
        public ICollection<CreateGameDto> Games { get; set; } = new List<CreateGameDto>();

    }
}
