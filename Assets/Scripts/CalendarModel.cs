using System;
using System.Collections.Generic;

public class CalendarModel
{
    public string calendarID;
    public List<int> claimedDays = new();
    public DateTime startDate;
}