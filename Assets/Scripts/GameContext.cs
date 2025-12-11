using System;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    [Header("Calendar")] [SerializeField] List<CalendarUIBind> calendars;

    [Header("Currency")] 
    [SerializeField]
    List<CurrencyUIBind> currencies;


    SaveService saveService;

    void Awake()
    {
        saveService = new SaveService();
        var currencyController = new CurrencyController(saveService);

        for (int i = 0; i < calendars.Count; i++)
        {
            var calendarController = new CalendarController(calendars[i].calendar, saveService);
            calendars[i].view.Construct(calendarController);
            calendars[i].view.Init();

            calendarController.OnDayRewardClaim += reward => { currencyController.UpdateCurrencyAmount(reward); };
        }

        for (int i = 0; i < currencies.Count; i++)
        {
            currencies[i].view.Construct(currencyController, currencies[i].currency);
            currencies[i].view.Init();
        }
    }
}

[Serializable]
public class CurrencyUIBind
{
    public CurrencyUI view;
    public CurrencyDefinition currency;
}

[Serializable]
public class CalendarUIBind
{
    public CalendarUI view;
    public CalendarDefinition calendar;
}