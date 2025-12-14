using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CalendarDefinition", menuName = "Scriptable Objects/CalendarDefinition")]
public class CalendarDefinition : ScriptableObject
{
    [SerializeField] string calendarID;
    [SerializeField] bool customStartDate;

    [Tooltip("Day-Month-Year")] [SerializeField]
    string startDate;

    [SerializeField] bool disablePastDays;
    [SerializeField] string calendarName;
    [SerializeField] int duration;
    [SerializeField] List<RewardDefinition> rewards;

    public string CalendarID => calendarID;
    public List<RewardDefinition> Rewards => rewards;
    public int Duration => duration;
    public string StartDate => startDate;
    public bool DisablePastDays => disablePastDays;
    public bool CustomStartDate => customStartDate;
    public string CalendarName => calendarName;

    void OnValidate()
    {
        while (rewards.Count < duration)
        {
            rewards.Add(new RewardDefinition());
        }

        while (rewards.Count > duration)
        {
            rewards.RemoveAt(rewards.Count - 1);
        }

        if (customStartDate)
        {
            if (string.IsNullOrEmpty(startDate))
                startDate = DateTime.Today.ToString("dd-MM-yyy");
        }

        if (!customStartDate)
        {
            startDate = null;
        }
    }
}