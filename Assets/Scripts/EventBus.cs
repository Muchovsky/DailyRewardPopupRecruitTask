using System;

public class EventBus
{
    public event Action<Reward> OnRewardClaimed;

    public void RewardClaimed(Reward reward)
    {
        OnRewardClaimed?.Invoke(reward);
    }
}