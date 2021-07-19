using System;
using System.Collections.Generic;
using System.Text;

namespace SecureFlight.Core.Entities
{
    public class Passenger : Person
    {
        public Passenger()
        {
            this.Flights = new HashSet<Flight>();
        }
        public ICollection<Flight> Flights { get; set; }

        public List<PassengerFlight> PassengerFlights { get; set; }
    }
}
