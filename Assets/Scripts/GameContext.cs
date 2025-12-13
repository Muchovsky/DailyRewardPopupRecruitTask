using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameContext : MonoBehaviour
{
    [Header("Calendar")] [SerializeField] List<CalendarUIBind> calendarsBind;

    [Header("Currency")] [SerializeField] List<CurrencyUIBind> currencies;


    SaveService saveService;
    CalendarSaveService calendarSaveService;
    CurrencySaveService currencySaveService;
    EventBus eventBus;


    void Awake()
    {
        saveService = new SaveService();
        calendarSaveService = new CalendarSaveService(saveService);
        currencySaveService = new CurrencySaveService(saveService);
        eventBus = new EventBus();

        foreach (var calendarBind in calendarsBind)
        {
            var calendarController = new CalendarController(calendarBind.calendar, calendarSaveService, eventBus);
            calendarBind.view.Construct(calendarController);
            calendarBind.view.Init();
            var bind = calendarBind;
            calendarBind.button.onClick.AddListener(() => OpenWindow(bind));
        }

        foreach (var currencyBind in currencies)
        {
            var currencyController = new CurrencyController(currencySaveService, currencyBind.view, currencyBind.currency, eventBus);
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