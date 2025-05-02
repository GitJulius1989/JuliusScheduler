using System;

namespace JuliusScheduler.Domain.Entities
{
    public class ScheduleOutput
    {
        public DateTimeOffset NextDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}