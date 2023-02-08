using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Common.Entities
{
    public class TournamentParameters : QueryStringParameters
    {
        public uint MinMonth { get; set; } = (uint)DateTime.Now.Month;
        public uint MaxMonth { get; set; } = (uint)DateTime.Now.Month + 12;
        public string Title { get; set; } = string.Empty;

        public bool IncludeGames { get; set; } = true;
    }
}
