using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CalendarDefinition", menuName = "Scriptable Objects/CalendarDefinition")]
public class CalendarDefinition : ScriptableObject
{
    [SerializeField] string calendarID;
   // [SerializeField] string startDate;
    [SerializeField] string calendarName;
    [SerializeField] int duration;
    [SerializeField] List<RewardQuantityPair> rewards;

    public string CalendarID => calendarID;
    public List<RewardQuantityPair> Rewards => rewards;
    public int Duration => duration;

    void OnValidate()
    {
        while (rewards.Count < duration)
        {
            rewards.Add(new RewardQuantityPair());
        }

        while (rewards.Count > duration)
        {
            rewards.RemoveAt(rewards.Count - 1);
        }
    }
}


[Serializable]
public class RewardQuantityPair
{
    [SerializeField] RewardDefinition reward;
    [SerializeField] int quantity;

    public RewardDefinition Reward => reward;
    public int Quantity => quantity;
}