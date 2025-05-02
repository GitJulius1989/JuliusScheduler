using System;
using JuliusScheduler.Domain.Enums;
using JuliusScheduler.Core.Strings;
using JuliusScheduler.Domain.Entities;
using JuliusScheduler.Core.Interfaces;

namespace JuliusScheduler.Core.Validators
{
    public class ScheduleValidator : IScheduleValidator
    {
        public bool Validate(ScheduleLimits limits, ScheduleConfig config, DateTimeOffset now, out string errorMessage)
        {
            //Date range
            if (!IsDateRangeValid(limits, config, now, out errorMessage))
            {
                return false;
            }

            // If its once, check if the nextdate is in the past
            if (config.ScheduleType == ScheduleType.Once)
            {
                var execTime = config.ExecutionTime.TimeOfDay;
                var nextDate = new DateTimeOffset(limits.StartDate.Date + execTime, now.Offset);

                if (!IsNextExecTimeOK(nextDate))
                {
                    errorMessage = ErrorMessages.InvalidExecutionTime;
                    return false;
                }
            }
            errorMessage = string.Empty;
            return true;
        }

        // Método que valida el rango de fechas
        public static bool IsDateRangeValid(ScheduleLimits limits, ScheduleConfig config, DateTimeOffset now, out string errorMessage)
        {
            errorMessage = string.Empty;

            var start = limits.StartDate.Date;
            var end = limits.EndDate.Date;

            // Si la fecha de inicio es anterior a hoy, no vale 
            if (start < DateTime.Today)
            {
                errorMessage = ErrorMessages.StartDateBeforeToday;
                return false;
            }

            // Si la fecha de fin es pasada, tampoco nos vale
            if (end < DateTime.Today)
            {
                errorMessage = ErrorMessages.EndDateBeforeToday;
                return false;
            }

            // Si la fecha final es anterior a la de inicio, inviable.
            if (end < start)
            {
                errorMessage = ErrorMessages.StartDateAfterEndDate;
                return false;
            }

            // Si las fechas coinciden, solo es válido si se trata de una única ejecución (Once)
            if (start == end && config.ScheduleType == ScheduleType.Recurring)
            {
                errorMessage = ErrorMessages.SameDateOnce;
                return false;
            }

            // Todo correcto
            return true;
        }

        // Método que valida si la fecha de siguiente ejecucion es correcta
        public static bool IsNextExecTimeOK(DateTimeOffset nextDateTime)
        {
            //Lógica de validacion DateTime
            return nextDateTime > DateTimeOffset.Now;
        }
    }
}