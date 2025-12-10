using System.Collections.Generic;
using UnityEngine;

public class CalendarUI : MonoBehaviour
{
    [SerializeField] List<DailyReward> rewardList;
    [SerializeField] DailyReward rewardPrefab;
    [SerializeField] GameObject rewardContainer;
    CalendarController calendarController;

    public void Construct(CalendarController controller)
    {
        calendarController = controller;
    }

    public void Init()
    {
        SetRewardPrefabs();
    }

    void Start()
    {
        SetRewards();
    }

    void SetRewardPrefabs()
    {
        var calendarLength = calendarController.CalendarLength();
        if (calendarLength < rewardList.Count)
        {
            var diff = rewardList.Count - calendarLength;
            for (int i = calendarLength; i < rewardList.Count; i++)
            {
                rewardList[i].gameObject.SetActive(false);
            }

            return;
        }

        if (calendarLength > rewardList.Count)
        {
            var diff = calendarLength - rewardList.Count;
            for (int i = 0; i < diff; i++)
            {
                var prefab = Instantiate(rewardPrefab, rewardContainer.transform, false);
                rewardList.Add(prefab);
            }

            return;
        }
    }

    void SetRewards()
    {
        for (var i = 0; i < calendarController.CalendarLength(); i++)
        {
            bool claimStatus = calendarController.IsDayClaimed(i);
            bool canBeClaimed = calendarController.CanClaimDay(i);
            var reward = calendarController.GetRewardForDay(i);
            rewardList[i].Init(reward, claimStatus, canBeClaimed);
        }
    }

    public void SimulateNextDay()
    {
        calendarController.SimulateDayPass();
        SetRewards();
    }

    public void RestoreCalendar()
    {
        calendarController.ResetDayOffset();
        SetRewards();
    }
}