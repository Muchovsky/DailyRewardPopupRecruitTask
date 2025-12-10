using System;

public class CalendarController
{
    CalendarDefinition calendarDefinition;
    SaveService saveService;
    CalendarState calendarState;


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
            calendarState.startDate = DateTime.Today.ToString("dd-MM-yyy");
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
        calendarState.claimedDays.Add(day);
        calendarState.lastClaimDate = DateTime.Today.ToString("dd-MM-yyy");
        saveService.SetCalendarStatus(calendarState);
    }

    public int GetCurrentDayIndex()
    {
        DateTime start = DateTime.Parse(calendarState.startDate);
        return (DateTime.Today - start).Days;
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

        return true;
    }

    public int CalendarLength()
    {
        return calendarDefinition.Duration;
    }

    public RewardQuantityPair GetRewardForDay(int day)
    {
        return calendarDefinition.Rewards[day];
    }
}