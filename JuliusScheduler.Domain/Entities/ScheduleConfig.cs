using System;
using JuliusScheduler.Domain.Enums;

namespace JuliusScheduler.Domain.Entities
{
    public class ScheduleConfig
    {
        public ScheduleType ScheduleType { get; set; }
        public DateTime ExecutionTime { get; set; }
        public ScheduleFrequency Frequency { get; set; }
        public int Interval { get; set; } = 1;
    }
}