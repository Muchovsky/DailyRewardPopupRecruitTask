using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    [SerializeField] Image rewardImage;
    [SerializeField] TextMeshProUGUI rewardQuantity;
    [SerializeField] TextMeshProUGUI dayLabel;
    [SerializeField] Image claimedImage;
    [SerializeField] Button claimButton;
    int day;
    
    public event Action<int> OnClicked;
    
    public void Init(int day)
    {
        this.day = day;
        claimButton.onClick.AddListener(RewardClick);
        dayLabel.text = $"Day {day.ToString()}";
    }
    public void UpdateView(Reward pair, bool claimStatus, bool canBeClaimed)
    {
        claimedImage.gameObject.SetActive(claimStatus);
        rewardImage.sprite = pair.Currency.Sprite;
        rewardQuantity.text = pair.Quantity.ToString();
        claimButton.interactable = canBeClaimed;
    }

    void RewardClick()
    {
        OnClicked?.Invoke(day);
        //PlaySound??
    }

    
}