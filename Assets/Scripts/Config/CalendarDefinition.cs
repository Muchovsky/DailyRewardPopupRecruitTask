using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CalendarDefinition", menuName = "Scriptable Objects/CalendarDefinition")]
public class CalendarDefinition : ScriptableObject
{
    [SerializeField] string calendarID;
   // [SerializeField] string startDate;
    [SerializeField] string calendarName;
    [SerializeField] int duration;
    [SerializeField] List<Reward> rewards;

    public string CalendarID => calendarID;
    public List<Reward> Rewards => rewards;
    public int Duration => duration;

    void OnValidate()
    {
        while (rewards.Count < duration)
        {
            rewards.Add(new Reward());
        }

        while (rewards.Count > duration)
        {
            rewards.RemoveAt(rewards.Count - 1);
        }
    }
}