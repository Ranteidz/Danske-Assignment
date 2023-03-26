using System;
using System.Collections.Generic;
using Company.API.Models;
using Company.API.Services;
using Company.API.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Xunit;

namespace Company.API.Tests;

public class ScheduleGeneratorTests
{
    private readonly IScheduleGenerator _scheduleGenerator;
    private readonly Mock<IScheduleProvider> _mockProvider;

    public ScheduleGeneratorTests()
    {
        _mockProvider = new Mock<IScheduleProvider>();

        var host = new HostBuilder().ConfigureServices(services =>
        {
            services.AddScoped<IScheduleGenerator, ScheduleGenerator>();
            services.AddScoped(x => _mockProvider.Object);
        }).Build();

        var serviceProvider = host.Services;

        _scheduleGenerator = serviceProvider.GetRequiredService<IScheduleGenerator>();
    }

    [Fact]
    public void Should_Create_Schedule()
    {
        var intervals = new List<int> {1, 5, 10, 15, 20};

        _mockProvider.Setup(x =>
            x.TryGetNotificationIntervals(It.Is<string>(s => s == "small"), It.Is<string>(s => s == "Denmark"),
                out intervals)).Returns(true);


        var companyDto = new CompanyDto
        {
            Market = "Denmark",
            Name = "Danske Bank",
            Number = 9999999999,
            Type = "small"
        };

        var currentTime = DateTime.Now;

        var schedule = _scheduleGenerator.CreateSchedule(companyDto);

        Assert.NotNull(schedule);
        Assert.Equal(5, schedule.Count);
        Assert.Equal(currentTime.AddDays(intervals[0]).ToString("dd/MM/YYYY"),
            schedule[0].DateTime.ToString("dd/MM/YYYY"));
        Assert.Equal(currentTime.AddDays(intervals[1]).ToString("dd/MM/YYYY"),
            schedule[1].DateTime.ToString("dd/MM/YYYY"));
        Assert.Equal(currentTime.AddDays(intervals[2]).ToString("dd/MM/YYYY"),
            schedule[2].DateTime.ToString("dd/MM/YYYY"));
        Assert.Equal(currentTime.AddDays(intervals[3]).ToString("dd/MM/YYYY"),
            schedule[3].DateTime.ToString("dd/MM/YYYY"));
        Assert.Equal(currentTime.AddDays(intervals[4]).ToString("dd/MM/YYYY"),
            schedule[4].DateTime.ToString("dd/MM/YYYY"));
    }

    [Fact]
    public void Should_Not_Create_Schedule_If_Requirements_Not_Met()
    {
        var intervals = new List<int> {1, 5, 10, 15, 20};

        _mockProvider.Setup(x =>
            x.TryGetNotificationIntervals(It.Is<string>(s => s == "small"), It.Is<string>(s => s == "Denmark"),
                out intervals)).Returns(true);


        var companyDto = new CompanyDto
        {
            Market = "Denmark",
            Name = "Danske Bank",
            Number = 9999999999,
            Type = "large"
        };

        var schedule = _scheduleGenerator.CreateSchedule(companyDto);

        Assert.Empty(schedule);
    }
}