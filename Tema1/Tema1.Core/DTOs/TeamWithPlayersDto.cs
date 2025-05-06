using System.Collections.Generic;
using Tema1.Core.DTOs;

namespace Tema1.Core.DTOs
{
    public class TeamWithPlayersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string League { get; set; }
        public int YearFounded { get; set; }
        public string Stadium { get; set; }
        public string CoachName { get; set; }

        public ICollection<PlayerDto> Players { get; set; }
    }
}