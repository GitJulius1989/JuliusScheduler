using System;
using JuliusScheduler.Domain.Enums;
using JuliusScheduler.Domain.Entities;

namespace JuliusScheduler.Core.Interfaces
{
    public interface IScheduleStrategy
    {
        // validacion de datos previa
        bool CanHandle(ScheduleConfig config);

        DateTimeOffset GetNextExecution(ScheduleLimits limits, ScheduleConfig config, DateTimeOffset now);
    }
}

