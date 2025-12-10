using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    [SerializeField] Image rewardImage;
    [SerializeField] TextMeshProUGUI rewardQuantity;
    [SerializeField] Image claimedImage;
    [SerializeField] Button claimButton;

    public void Init(RewardQuantityPair pair, bool claimStatus, bool canBeClaimed)
    {
        claimedImage.gameObject.SetActive(claimStatus);
        rewardImage.sprite = pair.Reward.Sprite;
        rewardQuantity.text = pair.Quantity.ToString();
        claimButton.interactable = canBeClaimed;
    }
}