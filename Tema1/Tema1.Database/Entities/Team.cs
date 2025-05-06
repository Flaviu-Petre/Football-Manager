using System;

namespace Tema1.Database.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string League { get; set; }
        public int YearFounded { get; set; }
        public string Stadium { get; set; }
        public string CoachName { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}