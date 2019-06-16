using System;
using System.Collections.Generic;

namespace Cafeteria.CoreLibs.DomainModel
{
    public class Availability
    {
        public IEnumerable<string> AvailabilityDays { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}