using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CalendarUI : MonoBehaviour
{
    [SerializeField] List<DailyReward> rewardList;
    [SerializeField] DailyReward rewardPrefab;
    [SerializeField] GameObject rewardContainer;
    [SerializeField] Button closeButton;
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    [SerializeField] ContentSizeFitter contentSizeFitter;
    CalendarController calendarController;
    [Header("Cheats")] [SerializeField] Button nextDayButton;
    [SerializeField] Button restoreButton;


    public void Construct(CalendarController controller)
    {
        calendarController = controller;
    }

    public void Init()
    {
        var prefabRect = rewardPrefab.GetComponent<RectTransform>();
        gridLayoutGroup.cellSize = prefabRect.sizeDelta;

        SetRewardPrefabs();

        for (int i = 0; i < rewardList.Count; i++)
        {
            int index = i;
            rewardList[i].Init(index);
            rewardList[i].OnClicked += OnDayClicked;
        }

        SetButtons();
    }

    void Start()
    {
        SetRewards();
        StartCoroutine(SetupGrid());
    }


    IEnumerator SetupGrid()
    {
        yield return new WaitForEndOfFrame();
        gridLayoutGroup.enabled = false;
        contentSizeFitter.enabled = false;
    }

    void SetRewardPrefabs()
    {
        rewardList.RemoveAll(item => item == null);

        foreach (Transform child in rewardContainer.transform)
        {
            var reward = child.GetComponent<DailyReward>();
            if (reward != null)
            {
                rewardList.Add(reward);
            }
            else
                child.gameObject.SetActive(false);
        }

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

    void SimulateNextDay()
    {
        calendarController.SimulateDayPass();
        SetRewards();
    }

    void RestoreCalendar()
    {
        calendarController.ResetDayOffset();
        SetRewards();
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    void SetButtons()
    {
        closeButton.onClick.AddListener(CloseWindow);
        nextDayButton.onClick.AddListener(SimulateNextDay);
        restoreButton.onClick.AddListener(RestoreCalendar);
    }

    void OnDestroy()
    {
        foreach (var dailyReward in rewardList)
        {
            dailyReward.OnClicked -= OnDayClicked;
        }
    }
}