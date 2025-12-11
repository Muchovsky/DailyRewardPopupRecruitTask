using System.Collections.Generic;
using UnityEngine;


public class SaveService
{
    string GetStartDayKey(string id) => id + "_START";
    string GetClaimedKey(string id) => id + "_CLAIMED";
    string GetLastDayKey(string id) => id + "_LAST";

    public int GetCurrencyAmount(string currencyName)
    {
        return PlayerPrefs.GetInt(currencyName, 0);
    }

    public void SetCurrencyAmount(string currencyName, int value)
    {
        PlayerPrefs.SetInt(currencyName, value);
        PlayerPrefs.Save();
    }

    public void UpdateCurrencyAmount(CurrencyNameEnum currencyNameEnum, int increaseBy)
    {
        string currencyName = currencyNameEnum.ToString();
        var currentAmount = PlayerPrefs.GetInt(currencyName, 0);
        var newAmount = currentAmount + increaseBy;
        PlayerPrefs.SetInt(currencyName, newAmount);
    }

    public CalendarState GetCalendarStatus(string calendarID)
    {
        var cs = new CalendarState
        {
            calendarID = calendarID,
            startDate = PlayerPrefs.GetString(GetStartDayKey(calendarID)),
            lastClaimDate = PlayerPrefs.GetString(GetLastDayKey(calendarID))
        };
        string claimedKeys = PlayerPrefs.GetString(GetClaimedKey(calendarID));
        if (!string.IsNullOrEmpty(claimedKeys))
        {
            string[] keys = claimedKeys.Split(',');
            foreach (string key in keys)
            {
                if (int.TryParse(key, out int day))
                    cs.claimedDays.Add(day);
            }
        }

        cs.claimedDays.Sort(); // just in case
        return cs;
    }

    public void SetCalendarStatus(CalendarState calendarState)
    {
        PlayerPrefs.SetString(GetStartDayKey(calendarState.calendarID), calendarState.startDate);
        PlayerPrefs.SetString(GetLastDayKey(calendarState.calendarID), calendarState.lastClaimDate);
        string claimed = string.Join(",", calendarState.claimedDays);
        PlayerPrefs.SetString(GetClaimedKey(calendarState.calendarID), claimed);
        PlayerPrefs.Save();
    }
}

public class CalendarState
{
    public string calendarID;
    public string startDate;
    public string lastClaimDate;
    public List<int> claimedDays = new List<int>();
}