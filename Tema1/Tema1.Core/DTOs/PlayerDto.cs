using System;

namespace Tema1.Core.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime DateOfBirth { get; set; }
        public int Age => CalculateAge(DateOfBirth);
        public string Nationality { get; set; }
        public string Position { get; set; }
        public int ShirtNumber { get; set; }
        public int GoalsScored { get; set; }
        public int Appearances { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}