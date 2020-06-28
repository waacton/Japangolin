using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ReactDemo.Controllers
{
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {
        private static List<CalendarEvent> AllEvents = ExampleData.CalendarEvents.AllEvents;

        [HttpGet("[action]")]
        public IEnumerable<CalendarEvent> Events()
        {
            return AllEvents;
        }
    }
}
