using System;
using JuliusScheduler.Domain.Entities;

namespace JuliusScheduler.Core.Interfaces
{

    public interface IScheduleValidator
    {
        bool Validate(ScheduleLimits limits, ScheduleConfig config, DateTimeOffset now, out string errorMessage);
    }
}