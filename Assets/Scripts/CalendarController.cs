using System;
using System.Globalization;
using UnityEngine;

public class CalendarController
{
    CalendarDefinition calendarDefinition;
    CalendarSaveService calendarSaveService;
    CalendarData calendarData;
    EventBus eventBus;
    int dayOffset;

    string dateFormat = "dd-MM-yyyy";
    CultureInfo cultureInfo = CultureInfo.InvariantCulture;

    public CalendarController(CalendarDefinition calendarDefinition, CalendarSaveService calendarSaveService, EventBus eventBus)
    {
        this.calendarDefinition = calendarDefinition;
        this.calendarSaveService = calendarSaveService;
        this.eventBus = eventBus;
        LoadCalendar();
    }

    void LoadCalendar()
    {
        calendarData = calendarSaveService.Load(calendarDefinition.CalendarID);
        if (string.IsNullOrEmpty(calendarData.startDate))
        {
            calendarData.startDate = calendarDefinition.CustomStartDate ? calendarDefinition.StartDate : DateTime.UtcNow.ToString(dateFormat, cultureInfo);
            calendarData.lastClaimDate = "";
            calendarSaveService.Save(calendarData);
        }
    }

    public bool IsDayClaimed(int day)
    {
        return calendarData.claimedDays.Contains(day);
    }

    public void ClaimDay(int day)
    {
        if (!CanClaimDay(day)) return;
        calendarData.claimedDays.Add(day);

        calendarData.lastClaimDate = DateTime.UtcNow.ToString(dateFormat, cultureInfo);
        calendarSaveService.Save(calendarData);


        eventBus.RewardClaimed(GetRewardForDay(day));
    }

    int GetCurrentDayIndex()
    {
        DateTime start = DateTime.ParseExact(calendarData.startDate, dateFormat, cultureInfo);
        var currentDay = (DateTime.UtcNow.Date - start).Days + dayOffset;
        return Mathf.Clamp(currentDay, 0, calendarDefinition.Duration - 1);
    }

    public bool CanClaimDay(int day)
    {
        if (IsDayClaimed(day))
        {
            return false;
        }

        if (day > GetCurrentDayIndex())
        {
            return false;
        }

        if (calendarDefinition.DisablePastDays)
        {
            if (day < GetCurrentDayIndex())
            {
                return false;
            }
        }

        return true;
    }

    public int CalendarLength()
    {
        return calendarDefinition.Duration;
    }

    public RewardDefinition GetRewardForDay(int day)
    {
        return calendarDefinition.Rewards[day];
    }

    public void SimulateDayPass()
    {
        dayOffset++;
    }

    public void ResetDayOffset()
    {
        dayOffset = 0;
    }
}