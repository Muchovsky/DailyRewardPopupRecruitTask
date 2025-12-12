using System;
using UnityEngine;

public class CalendarController
{
    CalendarDefinition calendarDefinition;
    SaveService saveService;
    CalendarState calendarState;
    int dayOffset;

    public event Action<Reward> OnDayRewardClaim;


    public CalendarController(CalendarDefinition calendarDefinition, SaveService saveService)
    {
        this.calendarDefinition = calendarDefinition;
        this.saveService = saveService;
        LoadCalendar();
    }

    void LoadCalendar()
    {
        calendarState = saveService.GetCalendarStatus(calendarDefinition.CalendarID);

        if (string.IsNullOrEmpty(calendarState.startDate))
        {
            calendarState.startDate = calendarDefinition.StartDate ?? DateTime.Today.ToString("dd-MM-yyy");

            calendarState.lastClaimDate = "";
            saveService.SetCalendarStatus(calendarState);
        }
    }

    public bool IsDayClaimed(int day)
    {
        return calendarState.claimedDays.Contains(day);
    }

    public void ClaimDay(int day)
    {
        if (!CanClaimDay(day)) return;
        calendarState.claimedDays.Add(day);
        calendarState.lastClaimDate = DateTime.Today.ToString("dd-MM-yyy");
        saveService.SetCalendarStatus(calendarState);

        OnDayRewardClaim?.Invoke(GetRewardForDay(day));
    }

    int GetCurrentDayIndex()
    {
        DateTime start = DateTime.Parse(calendarState.startDate);
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

    public Reward GetRewardForDay(int day)
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