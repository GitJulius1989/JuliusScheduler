using System;
using JuliusScheduler.Core.Enums;   

namespace JuliusScheduler.Core;



public class ScheduleConfig
{
    public ScheduleType Type { get; set; }
    public DateTime DateTime { get; set; } 
    public ScheduleFrecuency Occurs { get; set; }
}