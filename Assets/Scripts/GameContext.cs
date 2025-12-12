using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameContext : MonoBehaviour
{
    [FormerlySerializedAs("calendars")] [Header("Calendar")] [SerializeField] List<CalendarUIBind> calendarsBind;

    [Header("Currency")] [SerializeField] List<CurrencyUIBind> currencies;


    SaveService saveService;

    void Awake()
    {
        saveService = new SaveService();
        var currencyController = new CurrencyController(saveService);

        for (int i = 0; i < calendarsBind.Count; i++)
        {
            var calendarBind = calendarsBind[i];
            var calendarController = new CalendarController(calendarBind.calendar, saveService);
            calendarBind.view.Construct(calendarController);
            calendarBind.view.Init();
            calendarBind.button.onClick.AddListener(() => OpenWindow(calendarBind));

            calendarController.OnDayRewardClaim += reward => { currencyController.UpdateCurrencyAmount(reward); };
        }

        for (int i = 0; i < currencies.Count; i++)
        {
            currencies[i].view.Construct(currencyController, currencies[i].currency);
            currencies[i].view.Init();
        }
    }

    void OpenWindow(CalendarUIBind bind)
    {
        bind.view.OpenWindow();
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
    public Button button;
}