using UnityEngine;


[CreateAssetMenu(fileName = "RewardDefinition", menuName = "Scriptable Objects/RewardDefinition")]
public class RewardDefinition : ScriptableObject
{
    [SerializeField] string rewardName;
    [SerializeField] Sprite sprite;

    public string RewardName => rewardName;
    public Sprite Sprite => sprite;
}