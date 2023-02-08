using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Common.Dtos
{
    public class CreateGameDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
    }
}
