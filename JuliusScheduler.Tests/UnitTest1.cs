using System;
using Xunit;
using JuliusScheduler.Core;
using JuliusScheduler.Core.Enums;

namespace JuliusScheduler.Tests;

public class UnitTest1
{
    [Fact]
    public void ValidateDateRange_WithValidDates_True()
    {
        //Arrange

        DateTime startDate = new DateTime(2025, 12, 23);
        DateTime endDate = new DateTime(2026,12,23);
        string errorMessage;

        //Act
        bool isValidRange = ScheduleValidator.IsDateRangeValid(startDate, endDate, ScheduleType.Recurring, out errorMessage);

        //Assert
        Assert.True(isValidRange); // Verifica que rango es valido
        Assert.Equal(string.Empty, errorMessage); // Verifica que no hubo mensaje de error
    }

    //StartDate antiguo
    [Fact]
    public void ValidateDateRange_WithPastStartDate_False()
    {
        // Arrange
        DateTime startDate = new DateTime(2024, 1, 1); // pasada
        DateTime endDate = DateTime.Today.AddYears(1);
        string errorMessage;

        // Act
        bool isValidRange = ScheduleValidator.IsDateRangeValid(startDate, endDate, ScheduleType.Once, out errorMessage);

        // Assert
        Assert.False(isValidRange);
        Assert.Equal("StartDate can't be earlier than today", errorMessage);
    }


    //EndDate < StartDate
    [Fact]

    public void ValidateDateRange_EndBeforeStart_False()
    {
        // Arrange
        DateTime startDate = DateTime.Today.AddDays(10);
        DateTime endDate = DateTime.Today.AddDays(5);
        string errorMessage;

        // Act
        bool isValidRange = ScheduleValidator.IsDateRangeValid(startDate, endDate, ScheduleType.Once, out errorMessage);

        // Assert
        Assert.False(isValidRange);
        Assert.Equal("End date must be after start date", errorMessage);
    }

    //StartDate == EndDate valido para seleccion Once
    [Fact]

    public void ValidateDateRange_StartEqualsEnd_True()
    {
        // Arrange
        DateTime date = DateTime.Today.AddDays(5);
        string errorMessage;
        // Act
        bool isValidRange = ScheduleValidator.IsDateRangeValid(date, date, ScheduleType.Once, out errorMessage);
        // Assert
        Assert.True(isValidRange);
        Assert.Equal(string.Empty, errorMessage);
    }

    //StartDate == EndDate erroneo para seleccion Recurrent
    [Fact]

    public void ValidateDateRange_StartEqualsEnd_False()
    {
        // Arrange
        DateTime date = DateTime.Today.AddDays(5);
        string errorMessage;

        // Act
        bool isValidRange = ScheduleValidator.IsDateRangeValid(date, date, ScheduleType.Recurring, out errorMessage);

        // Assert
        Assert.False(isValidRange);
        Assert.Equal("If Start and End are in the same day, select once type not recurring", errorMessage);
    }

}

