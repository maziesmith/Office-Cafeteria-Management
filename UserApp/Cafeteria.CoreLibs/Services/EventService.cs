using System;
using System.Collections.Generic;
using Cafeteria.CoreLibs.Abstractions;
using Cafeteria.CoreLibs.DomainModel;

namespace Cafeteria.CoreLibs.Services
{
    public class EventService : IEventService
    {
        public IEnumerable<Event> GetAllEvents()
        {
            return new[]
            {
                new Event
                {
                    StartTime = DateTime.Now.AddHours(2),
                    EndTime = DateTime.Now.AddHours(4),
                    Description = "Townhall by senior management"
                },

                new Event
                {
                    StartTime = DateTime.Now.AddDays(1).AddHours(1),
                    EndTime = DateTime.Now.AddDays(1).AddHours(3),
                    Description = "Tech talk for IST"
                },

                new Event
                {
                    StartTime = DateTime.Now.AddDays(4).AddHours(2),
                    EndTime = DateTime.Now.AddDays(4).AddHours(3),
                    Description = "Toastmasters session"
                },

                new Event
                {
                    StartTime = DateTime.Now.AddDays(5).AddHours(3),
                    EndTime = DateTime.Now.AddDays(5).AddHours(6),
                    Description = "Grad presentation event"
                },

            };
        }
    }
}