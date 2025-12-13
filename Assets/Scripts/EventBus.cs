using System;

public class EventBus
{
    public event Action<RewardDefinition> OnRewardClaimed;

    public void RewardClaimed(RewardDefinition rewardDefinition)
    {
        OnRewardClaimed?.Invoke(rewardDefinition);
    }
}