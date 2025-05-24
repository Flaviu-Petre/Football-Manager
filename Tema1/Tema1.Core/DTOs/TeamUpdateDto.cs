using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1.Core.DTOs
{
    public class TeamUpdateDto
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? League { get; set; }
        public int? YearFounded { get; set; }
        public string? Stadium { get; set; }
        public string? CoachName { get; set; }
    }
}
