using System;

namespace Cafeteria.CoreLibs.DomainModel
{
    public class Event
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Description { get; set; }
    }
}