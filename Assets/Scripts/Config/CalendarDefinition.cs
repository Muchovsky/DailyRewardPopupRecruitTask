using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CalendarDefinition", menuName = "Scriptable Objects/CalendarDefinition")]
public class CalendarDefinition : ScriptableObject
{
    [SerializeField] string calendarName;
    [SerializeField] int duration;
    [SerializeField] List<RewardQuantityPair> rewards;
    
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