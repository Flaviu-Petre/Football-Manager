using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1.Core.DTOs
{
    public class PlayerUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string? Position { get; set; }
        public int? ShirtNumber { get; set; }
        public int? GoalsScored { get; set; }
        public int? Appearances { get; set; }
        public int? TeamId { get; set; }
    }
}
