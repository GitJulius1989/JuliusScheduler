using System;
using Xunit;
using JuliusScheduler.Domain.Entities;
using JuliusScheduler.Domain.Enums;
using JuliusScheduler.Core.Services;
using FluentAssertions;

namespace JuliusScheduler.Tests
{
    public class ScheduleCalculatorTests_Once
    {
        [Fact]
        public void OnceSchedule_ReturnsNextExecutionDate()
        {
            // Arrange: límites y config para Once
            var limits = new ScheduleLimits
            {
                StartDate = new DateTime(2025, 10, 5),
                EndDate   = new DateTime(2025, 10, 5)
            };
            var config = new ScheduleConfig
            {
                ScheduleType  = ScheduleType.Once,
                ExecutionTime = new DateTime(2025, 10, 5, 14, 0, 0)
            };

            var calc = new ScheduleCalculator();

            // Act
            var result = calc.CalculateNextDate(limits, config);

            // Assert
            var expected = new DateTimeOffset(2025, 10, 5, 14, 0, 0, TimeSpan.Zero);
            result.NextDate.Should().Be(expected);
            result.Description.Should().Contain("Occurs once");
        }

        [Fact]
        public void RecurringSchedule_ReturnsNextExecutionDate()
        {
            // Arrange: límites y config para Recurring every 2 days
            var limits = new ScheduleLimits
            {
                StartDate = new DateTime(2025, 6, 1),
                EndDate   = new DateTime(2025, 6, 30)
            };
            var config = new ScheduleConfig
            {
                ScheduleType  = ScheduleType.Recurring,
                ExecutionTime = new DateTime(2025, 6, 1, 8, 0, 0),
                Frequency     = ScheduleFrequency.Daily,
                Interval      = 2
            };

            var calc = new ScheduleCalculator();

            // Act
            var output = calc.CalculateNextDate(limits, config);

            // Assert: la siguiente ejecución tras 2025-06-01 a las 08:00 será el 2025-06-03
            var expected = new DateTimeOffset(2025, 6, 3, 8, 0, 0, TimeSpan.Zero);
            output.NextDate.Should().Be(expected);
            output.Description.Should().Contain("Occurs every");
        }
    }
}
