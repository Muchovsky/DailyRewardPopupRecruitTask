using System;
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

        for (int i = 0; i < rewardList.Count; i++)
        {
            int index = i;
            rewardList[i].Init(index);
            rewardList[i].OnClicked += OnDayClicked;
        }
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
        }
    }

    void SetRewards()
    {
        for (var i = 0; i < calendarController.CalendarLength(); i++)
        {
            SetReward(i);
        }
    }

    void SetReward(int i)
    {
        bool claimStatus = calendarController.IsDayClaimed(i);
        bool canBeClaimed = calendarController.CanClaimDay(i);
        var reward = calendarController.GetRewardForDay(i);
        rewardList[i].UpdateView(reward, claimStatus, canBeClaimed);
    }

    void OnDayClicked(int day)
    {
        calendarController.ClaimDay(day);
        SetReward(day);
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

    void OnDestroy()
    {
        foreach (var dailyReward in rewardList)
        {
            dailyReward.OnClicked -= OnDayClicked;
        }
    }
}