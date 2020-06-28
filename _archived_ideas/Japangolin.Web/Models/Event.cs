using System;

namespace Models
{
    public class CalendarEvent
    {
        public string Title {get;set;}= string.Empty;
        public bool AllDay {get;set;}
        public DateTime Start {get;set;}
        public DateTime End {get;set;}
        public string Desc {get;set;} = string.Empty;
    }
}