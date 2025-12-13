using System;
using System.Globalization;
using Codice.CM.Common.Tree;
using Unity.VisualScripting;
using UnityEditor;


[CustomEditor(typeof(CalendarDefinition))]
public class CalendarDefinitionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var calendar = (CalendarDefinition)target;

        CheckID(calendar);
        CheckStartDate(calendar);
        CheckRewards(calendar);
    }

    void CheckID(CalendarDefinition calendar)
    {
        if (string.IsNullOrEmpty(calendar.CalendarID))
            EditorGUILayout.HelpBox("CalendarID can't be empty.", MessageType.Error);
    }

    void CheckStartDate(CalendarDefinition calendar)
    {
        if (!string.IsNullOrEmpty(calendar.StartDate))
        {
            if (!DateTime.TryParseExact(calendar.StartDate, "dd-MM-yyyy", null, DateTimeStyles.None, out _))
            {
                EditorGUILayout.HelpBox("Start Date format is invalid it must be DD-MM-YYYY.", MessageType.Error);
            }

            EditorGUILayout.HelpBox("Start Date is correct", MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("Start Date is empty. It will be set at calendar open.", MessageType.Warning);
        }
    }

    void CheckRewards(CalendarDefinition calendar)
    {
        if (calendar.Rewards.Count == 0)
            return;

        for (int i = 0; i < calendar.Rewards.Count; i++)
        {
            var reward = calendar.Rewards[i];

            if (reward.Currency == null)
            {
                EditorGUILayout.HelpBox($"Reward at {i} is empty .", MessageType.Error);
                break;
            }

            if (reward.Quantity == 0)
            {
                EditorGUILayout.HelpBox($"Reward at {i} have O quantity.", MessageType.Error);
            }
        }
    }
}