using System.ComponentModel.DataAnnotations;

namespace Lms.Core.Models.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public int TournamentId { get; set; }
    }
}
