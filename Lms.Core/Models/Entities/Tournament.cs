namespace Lms.Core.Models.Entities
{
    /// <summary>
    /// Base class for tournaments
    /// </summary>
    public class Tournament
    {
        /// <summary>
        /// Tournament Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tournament title
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Tournament start date/time
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Tournament games
        /// </summary>
        public ICollection<Game>? Games { get; set; }
    }
}
