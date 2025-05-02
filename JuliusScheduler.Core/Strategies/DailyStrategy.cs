using System;
using JuliusScheduler.Domain.Enums;
using JuliusScheduler.Domain.Entities;
using JuliusScheduler.Core.Interfaces;

namespace JuliusScheduler.Core.Strategies
{
    public class DailyStrategy : IScheduleStrategy
    {

        public bool CanHandle(ScheduleConfig config)
        {
            return (config.ScheduleType == ScheduleType.Recurring && config.Frequency == ScheduleFrequency.Daily);
        }


        public DateTimeOffset GetNextExecution(ScheduleLimits limits, ScheduleConfig config, DateTimeOffset now)
        {
            var firstExecution = new DateTimeOffset(limits.StartDate.Year, limits.StartDate.Month, limits.StartDate.Day, 0, 0, 0, now.Offset);
            firstExecution = firstExecution.Add(config.ExecutionTime.TimeOfDay);

            DateTimeOffset nextExecution;

            if (now < firstExecution)
            {
                nextExecution = firstExecution;
            }
            else
            {
                double daysSinceFirstExecution = (now - firstExecution).TotalDays;
                double intervalsPassed = Math.Ceiling(daysSinceFirstExecution / config.Interval);
                nextExecution = firstExecution.AddDays(intervalsPassed * config.Interval);
            }
            if (nextExecution > limits.EndDate)
            {
                throw new InvalidOperationException("No more executions available within the specified limits.");
            }
            return nextExecution;
        }
    }
}