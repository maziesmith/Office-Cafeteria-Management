using System;
using System.Collections.ObjectModel;
using System.Linq;
using Cafeteria.CoreLibs.Abstractions;
using Cafeteria.CoreLibs.DomainModel;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms.Internals;

namespace Cafeteria.SharedView.ViewModel
{
    public class CafeteriaEventsViewModel
    {
        private readonly IEventService _eventService;

        public CafeteriaEventsViewModel(IEventService eventService)
        {
            _eventService = eventService;
            PopulateEvents();
        }

        private void PopulateEvents()
        {
            var events = _eventService.GetAllEvents();
            TodayEvents = new ObservableCollection<Event>(events.Where(evnt => evnt.StartTime.Date == DateTime.Today));
            AllEvents = new CalendarEventCollection();
            events.ForEach(evnt => AllEvents.Add(new CalendarInlineEvent
            {
                StartTime = evnt.StartTime,
                EndTime = evnt.EndTime,
                Subject = evnt.Description
            }));
        }

        public ObservableCollection<Event> TodayEvents { get; private set; }

        public CalendarEventCollection AllEvents { get; private set; }


    }
}
