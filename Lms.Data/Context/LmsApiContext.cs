using Lms.Common.Entities;
using Lms.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Context
{
    public class LmsApiContext : DbContext
    {
        public LmsApiContext(DbContextOptions<LmsApiContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament> Tournament { get; set; } = default!;

        public DbSet<Game> Game { get; set; } = default!;
    }

}
