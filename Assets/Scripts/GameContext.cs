using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    [Header("Calendar")] [SerializeField] List<CalendarDefinition> calendarDefinitions;
    [SerializeField] List<CalendarUI> calendarViews;

    [Header("Services")] SaveService saveService;

    void Awake()
    {
        saveService = new SaveService();
        for (int i = 0; i < calendarDefinitions.Count; i++)
        {
            var calendarController = new CalendarController(calendarDefinitions[i], saveService);
            calendarViews[i].Construct(calendarController);
            calendarViews[i].Init();
        }
    }
}