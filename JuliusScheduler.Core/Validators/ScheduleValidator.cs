using JuliusScheduler.Core.Enums;

namespace JuliusScheduler.Core;



public static class ScheduleValidator 
{
    // Método que valida el rango de fechas
    public static bool IsDateRangeValid(DateTime startDate, DateTime endDate, ScheduleType type, out string errorMessage)
    {
        errorMessage = string.Empty;

        // Si la fecha de inicio es anterior a hoy, no vale
        if (startDate.Date < DateTime.Today)
        {
            errorMessage = "StartDate can't be earlier than today";
            return false;
        }

        // Si la fecha de fin también es pasada, tampoco nos vale
        if (endDate.Date < DateTime.Today)
        {
            errorMessage = "EndDate can't be earlier than today";
            return false;
        }

        // Si la fecha final es anterior a la de inicio, cuidado, error de lógica
        if (endDate.Date < startDate.Date)
        {
            errorMessage = "End date must be after start date";
            return false;
        }

        // Si las fechas coinciden, solo es válido si se trata de una única ejecución (Once)
        if (startDate.Date == endDate.Date && type == ScheduleType.Recurring)
        {
            errorMessage = "If Start and End are in the same day, select once type not recurring";
            return false;
        }

        // Todo correcto
        return true;
    }

    // Método que valida si la fecha es correcta
    public static bool IsDateTimeOK(DateTime dateTime)
    {
        //Lógica de validacion DateTime
        return dateTime > DateTime.Now;
    }


}