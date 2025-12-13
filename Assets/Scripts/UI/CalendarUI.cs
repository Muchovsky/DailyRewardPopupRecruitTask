using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CalendarUI : MonoBehaviour
{
    [SerializeField] Button closeButton;
    [SerializeField] List<DailyRewardUI> rewardList;

    [FormerlySerializedAs("rewardPrefab")] [SerializeField]
    DailyRewardUI rewardUIPrefab;

    [SerializeField] GameObject rewardContainer;
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    [SerializeField] ContentSizeFitter contentSizeFitter;
    [SerializeField] ScrollRect scroll;
    [Header("Cheats")] [SerializeField] Button nextDayButton;
    [SerializeField] Button restoreButton;

    CalendarController calendarController;

    void Start()
    {
        SetRewards();
        StartCoroutine(SetupGrid());
    }

    void OnDestroy()
    {
        foreach (var dailyReward in rewardList) dailyReward.OnClicked -= OnDayClicked;
    }

    public void Construct(CalendarController controller)
    {
        calendarController = controller;
    }

    public void Init()
    {
        var prefabRect = rewardUIPrefab.GetComponent<RectTransform>();
        gridLayoutGroup.cellSize = prefabRect.sizeDelta;

        SetRewardPrefabsList();

        for (var i = 0; i < rewardList.Count; i++)
        {
            var index = i;
            rewardList[i].Init(index);
            rewardList[i].OnClicked += OnDayClicked;
        }

        SetButtons();
    }

    IEnumerator SetupGrid()
    {
        yield return null;
        gridLayoutGroup.enabled = false;
        contentSizeFitter.enabled = false;
        scroll.verticalNormalizedPosition = 1;
    }

    void SetRewardPrefabsList()
    {
        RemoveEmptyElements();
        AssignLoosePrefabs();
        AdjustPrefabsCont(calendarController.CalendarLength());
    }

    void AdjustPrefabsCont(int calendarLength)
    {
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
            for (var i = 0; i < diff; i++)
            {
                var prefab = Instantiate(rewardUIPrefab, rewardContainer.transform, false);
                rewardList.Add(prefab);
            }
        }
    }

    void AssignLoosePrefabs()
    {
        foreach (Transform child in rewardContainer.transform)
        {
            var reward = child.GetComponent<DailyRewardUI>();
            if (reward != null)
                rewardList.Add(reward);
            else
                child.gameObject.SetActive(false);
        }
    }

    void RemoveEmptyElements()
    {
        rewardList.RemoveAll(item => item == null);
    }

    void SetRewards()
    {
        for (var i = 0; i < calendarController.CalendarLength(); i++) 
            SetReward(i);
    }

    void SetReward(int i)
    {
        var claimStatus = calendarController.IsDayClaimed(i);
        var canBeClaimed = calendarController.CanClaimDay(i);
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
}