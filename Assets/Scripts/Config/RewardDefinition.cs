using UnityEngine;


[CreateAssetMenu(fileName = "RewardDefinition", menuName = "Scriptable Objects/RewardDefinition")]
public class RewardDefinition : ScriptableObject
{
    [SerializeField] string rewardName;
    [SerializeField] Sprite sprite;
}