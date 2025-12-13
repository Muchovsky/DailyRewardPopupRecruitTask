using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

public class CalendarControllerTests
{
    FakeCalendarSaveService saveService;
    CalendarController controller;
    EventBus eventBus;


    [SetUp]
    public void Setup()
    {
        saveService = new FakeCalendarSaveService
        {
            FakeCalendarData = new CalendarData
            {
                calendarID = "TEST",
                startDate = DateTime.Today.ToString("dd-MM-yyy"),
                claimedDays = new List<int>()
            }
        };

        eventBus = new EventBus();

        var definition = ScriptableObject.CreateInstance<CalendarDefinition>();
        var rewards = new List<RewardDefinition>();

        var currencyDef = ScriptableObject.CreateInstance<CurrencyDefinition>();
        typeof(CurrencyDefinition)
            .GetField("currencyName", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(currencyDef, CurrencyNameEnum.Gold);


        for (int i = 0; i < 7; i++)
        {
            var reward = new RewardDefinition();

            typeof(RewardDefinition)
                .GetField("currency", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(reward, currencyDef);

            typeof(RewardDefinition)
                .GetField("quantity", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(reward, 10);
            rewards.Add(reward);
        }

        typeof(CalendarDefinition)
            .GetField("rewards", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(definition, rewards);

        typeof(CalendarDefinition)
            .GetField("duration", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(definition, 7);

        typeof(CalendarDefinition)
            .GetField("calendarID", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(definition, "TEST");

        controller = new CalendarController(definition, saveService, eventBus);
    }

    [Test]
    public void ClaimDay_MarksDayAsClaimed()
    {
        controller.ClaimDay(0);

        Assert.IsTrue(controller.IsDayClaimed(0));
    }

    [Test]
    public void CannotClaimSameDayTwice()
    {
        controller.ClaimDay(0);

        Assert.IsFalse(controller.CanClaimDay(0));
    }
    
    [Test]
    public void ClaimDay_WhenClaimed_RaisesRewardClaimedEvent()
    {
        int eventCallCount = 0;
        RewardDefinition receivedReward = null;
        eventBus.OnRewardClaimed += reward =>
        {
            eventCallCount++;
            receivedReward = reward;
        };
        controller.ClaimDay(0);
        controller.ClaimDay(0);
        Assert.IsTrue(controller.IsDayClaimed(0));
        Assert.AreEqual(1, eventCallCount);
        Assert.AreEqual(CurrencyNameEnum.Gold, receivedReward.Currency.CurrencyName);
        Assert.AreEqual(10, receivedReward.Quantity);
    }

    [Test]
    public void CanClaimDay_WhenPreviousDayNotClaimed_ReturnsFalse()
    {
        Assert.IsFalse(controller.CanClaimDay(1));
    }
}

public class FakeCalendarSaveService : CalendarSaveService
{
    public CalendarData FakeCalendarData;


    public FakeCalendarSaveService() : base(null)
    {
    }

    public override CalendarData Load(string calendarID)
    {
        return FakeCalendarData ?? new CalendarData
        {
            calendarID = calendarID,
            claimedDays = new List<int>()
        };
    }

    public override void Save(CalendarData state)
    {
        FakeCalendarData = state;
    }
}