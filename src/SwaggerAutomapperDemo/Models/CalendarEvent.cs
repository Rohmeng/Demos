using System;
namespace SwaggerAutomapperDemo.Models
{
    /// <summary>
    /// Calendar event. 由简到繁映射
    /// </summary>
    public class CalendarEvent
    {
        public DateTime EventDate { get; set; }
        public string Title { get; set; }
    }

    public class CalendarEventForm
    {
        public DateTime EventDate { get; set; }
        public int EventHour { get; set; }
        public int EventMinute { get; set; }
        public string Title { get; set; }
    }
}
