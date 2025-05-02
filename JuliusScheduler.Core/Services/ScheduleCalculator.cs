using System;
using JuliusScheduler.Domain.Enums;
using JuliusScheduler.Domain.Entities;
using JuliusScheduler.Core.Validators;
using JuliusScheduler.Core.Interfaces;


namespace JuliusScheduler.Core.Services

{
    public class ScheduleCalculator : IScheduleCalculator
    {
        public ScheduleOutput CalculateNextDate(ScheduleLimits limits, ScheduleConfig config)
        {
            var now = DateTimeOffset.Now;
            //Validacion de fechas preconstruccion
            if (!ScheduleValidator.IsDateRangeValid(limits, config, now, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            //Fork según tipo
            switch (config.ScheduleType)
            {
                case ScheduleType.Once:
                    {
                        DateTimeOffset nextExecutionDate = CalculateOnce(limits, config, now);
                        return FormatOnce(limits, config, nextExecutionDate);
                    }

                case ScheduleType.Recurring:
                    {
                        DateTimeOffset nextExecutionDate = CalculateRecurring(limits, config, now);
                        return FormatRecurring(limits, config, nextExecutionDate);
                    }
                default:
                throw new ArgumentException(nameof(config.ScheduleType));
            }
        }

            public DateTimeOffset CalculateOnce (ScheduleLimits limits, ScheduleConfig config, DateTimeOffset now)
            {
                var nextExecutionDate = new DateTimeOffset ( limits.StartDate.Date + config.ExecutionTime.TimeOfDay,  TimeSpan.Zero);
                if (nextExecutionDate < now)
                {
                    throw new InvalidOperationException();
                } 
                return nextExecutionDate;
            }

            public DateTimeOffset CalculateRecurring (ScheduleLimits limits, ScheduleConfig config, DateTimeOffset now)
            {
                var nextExecutionDate = new DateTimeOffset ( limits.StartDate.Date + config.ExecutionTime.TimeOfDay,  TimeSpan.Zero);
                int iteration = 1;

                do
                {
                   switch (config.Frequency)
                   {
                    case ScheduleFrequency.Daily:
                        nextExecutionDate = nextExecutionDate.AddDays (config.Interval);
                        break;
                    case ScheduleFrequency.Weekly:
                        nextExecutionDate = nextExecutionDate.AddDays (config.Interval*7);
                        break;
                    case ScheduleFrequency.Monthly:
                        nextExecutionDate = nextExecutionDate.AddDays (config.Interval*30);
                        break;
                    case ScheduleFrequency.Yearly:
                        nextExecutionDate = nextExecutionDate.AddDays (config.Interval*365);
                        break;
                 
                   default:
                   throw new ArgumentOutOfRangeException(nameof(config.Frequency));
                    }
                    iteration++;
                }
                while (nextExecutionDate < now);
                return nextExecutionDate;
            }

            public ScheduleOutput FormatOnce (ScheduleLimits limits, ScheduleConfig config,DateTimeOffset nextExecutionDate)
            {
                return new ScheduleOutput
                {
                    NextDate = nextExecutionDate,
                    Description = $"Occurs once. Schedule will be used on {nextExecutionDate:MM/dd/yyyy} at {nextExecutionDate:HH:mm} starting on {limits.StartDate:MM/dd/yyyy}"
                };
            }

            public ScheduleOutput FormatRecurring (ScheduleLimits limits, ScheduleConfig config,DateTimeOffset nextExecutionDate)
            {
                string singularFrequencyText = "";
                string pluralFrequencyText = "";

            switch (config.Frequency)
            {
                case ScheduleFrequency.Daily:
                    singularFrequencyText = "day";
                    pluralFrequencyText = "days";
                    break;
                case ScheduleFrequency.Weekly:
                    singularFrequencyText = "week";
                    pluralFrequencyText = "weeks";
                    break;
                case ScheduleFrequency.Monthly:
                    singularFrequencyText = "month";
                    pluralFrequencyText = "months";
                    break;
                case ScheduleFrequency.Yearly:
                    singularFrequencyText = "year";
                    pluralFrequencyText = "years";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(config.Frequency));
            }

            string description="";
            if(config.Interval == 1 )
            {
                description = $"Occurs every {singularFrequencyText}. Schedule will be used on {nextExecutionDate:MM/dd/yyyy} at {nextExecutionDate:HH:mm} starting on {limits.StartDate:MM/dd/yyyy}";
            }else
            {
                description = $"Occurs every {config.Interval} {pluralFrequencyText}. Schedule will be used on {nextExecutionDate:MM/dd/yyyy} at {nextExecutionDate:HH:mm} starting on {limits.StartDate:MM/dd/yyyy}";
            }
            return new ScheduleOutput
            {
                NextDate = nextExecutionDate,
                Description = description 
            };
        
        }
    }
}
