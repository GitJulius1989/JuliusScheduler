using System;
using JuliusScheduler.Domain.Entities;

namespace JuliusScheduler.Core.Interfaces
{
    public interface IScheduleCalculator
    {
        JuliusScheduler.Domain.Entities.ScheduleOutput
            CalculateNextDate(ScheduleLimits limits, ScheduleConfig config);
    }
}