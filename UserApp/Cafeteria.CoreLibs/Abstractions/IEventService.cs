using System.Collections.Generic;
using Cafeteria.CoreLibs.DomainModel;

namespace Cafeteria.CoreLibs.Abstractions
{
    public interface IEventService
    {
        IEnumerable<Event> GetAllEvents();
    }
}