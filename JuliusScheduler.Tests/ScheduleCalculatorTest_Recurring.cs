using System;
using Xunit;
using JuliusScheduler.Domain.Entities;
using JuliusScheduler.Domain.Enums;
using JuliusScheduler.Core.Validators;

namespace JuliusScheduler.Tests
{
    public class ScheduleValidatorTests_Recurring
    {
        [Fact]
        public void ValidateDateRange_ValidRecurringRange_True()
        {
            // Arrange
            var limits = new ScheduleLimits
            {
                StartDate = new DateTime(2025, 6, 1),
                EndDate   = new DateTime(2025, 6, 6)  // rango de varios días
            };
            var config = new ScheduleConfig
            {
                ScheduleType  = ScheduleType.Recurring,
                ExecutionTime = new DateTime(2025, 6, 1, 8, 0, 0),
                Frequency     = ScheduleFrequency.Daily,
                Interval      = 1
            };
            // 'now' fijo
            var now = new DateTimeOffset(new DateTime(2025, 6, 1, 0, 0, 0), TimeSpan.Zero);
            string errorMessage;

            // Act
            bool isValidRange = ScheduleValidator
                .IsDateRangeValid(limits, config, now, out errorMessage);

            // Assert
            Assert.True(isValidRange, "Un rango de varios días debe ser válido para Recurring");
            Assert.Equal(string.Empty, errorMessage);
        }
    }
}
