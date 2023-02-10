using System.ComponentModel.DataAnnotations;

namespace Lms.Common.Dtos
{
    /// <summary>
    /// Dto for tournament
    /// </summary>
    public class TournamentDto
    {
        /// <summary>
        /// Tournament Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tournament Title
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// tournament starting date/time
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// tournament end date/time
        /// </summary>
        public DateTime EndDate => StartDate.AddMonths(3);
        /// <summary>
        /// games during the tournament
        /// </summary>
        public ICollection<GameDto>? Games { get; set; }
    }
}
