using System;

namespace Tema1.Core.DTOs
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string League { get; set; }
        public int YearFounded { get; set; }
        public string Stadium { get; set; }
        public string CoachName { get; set; }
    }
}