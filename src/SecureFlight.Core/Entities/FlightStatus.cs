using System;
using System.Collections.Generic;
using System.Text;

namespace SecureFlight.Core.Entities
{
    public class FlightStatus
    {
        public FlightStatus()
        {
            this.Flights = new HashSet<Flight>();
        }

        public Enums.FlightStatus Id { get; set; }

        public string Description { get; set; }

        public ICollection<Flight> Flights { get; set; }
    }
}
