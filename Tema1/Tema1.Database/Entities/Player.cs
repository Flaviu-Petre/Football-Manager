using System;


namespace Tema1.Database.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public int ShirtNumber { get; set; }
        public int GoalsScored { get; set; }
        public int Appearances { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}