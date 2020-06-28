using System;
using System.Collections.Generic;

using Models;

namespace ExampleData
{
    public static class CalendarEvents
    {
        public static List<CalendarEvent> AllEvents =
        new List<CalendarEvent>() {
            new CalendarEvent() {
                Title = "All Day Event",
                Start = new DateTime(2017, 7, 1),
                End = new DateTime(2017, 7, 1),
                AllDay = true
            },

            new CalendarEvent() {
                Title = "Long Event",
                Start = new DateTime(2017, 7, 7),
                End = new DateTime(2017, 7, 10)
            },

            new CalendarEvent() {
                Title = "DTS STARTS",
                Start = new DateTime(2017, 7, 13, 0, 0, 0),
                End = new DateTime(2017, 7, 20, 0, 0, 0)
            },

            new CalendarEvent() {
                Title = "DTS ENDS",
                Start =  new DateTime(2017, 7, 6, 0, 0, 0),
                End = new DateTime(2017, 7, 13, 0, 0, 0)
            }
        };
    }
}
