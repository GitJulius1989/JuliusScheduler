using System;
using Xunit;
using JuliusScheduler.Core;
using JuliusScheduler.Core.Enums;
using JuliusScheduler.Core.Entities;
using JuliusScheduler.Core.Services;
using JuliusScheduler.Core.Validators;

public class ScheduleCalculatorTests
{
    
    [Fact]
    // Test para verificar la validez de un rango de fechas
    public void TestScheduleCalculator()
    {
        // Arrange
        // Creo configuracion que simula un evento unico (Once)
        var config = new ScheduleConfig
        {
            StartDate = new DateTime(2025,10,5),
            EndDate = new DateTime(2025,10,5),
            Type = ScheduleType.Once,
        };
        //Invento que la fecha actual es el dia anterior a la solicitud
        var CurrentDate = new DateTime(2025, 10,4);

        //Llamo a scheduleCalculator (aun en desarrollo) y le proporciono los argumentos declarados
        var scheduleCalculator = new ScheduleCalculator(config, CurrentDate);

        // Act
        // Aquí llamo al método a testar
        var result = scheduleCalculator.ScheduleCalculator(config, CurrentDate);

        // Assert
        // Verifico que el resultado sea lo que se espera
        Assert.Equal(new DateTime(2025,12,1), result.NextDate);
        // Verifico que en el texto devuelto aparece "Once"
        Assert.Contains("Once", result.Description, StringComparison.OrdinalIgnoreCase);
        // StringComparison.OrdinalIgnoreCase
        //compara los valores binarios de los caracteres de las cadenas,
        //ignorando las diferencias entre letras mayúsculas y minúsculas
    }
}
{
	public Class1()
	{
	}
}
