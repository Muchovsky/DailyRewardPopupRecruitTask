using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CurrencyDefinition", menuName = "Scriptable Objects/CurrencyDefinition")]
public class CurrencyDefinition : ScriptableObject
{
    [FormerlySerializedAs("rewardName")] [SerializeField]
    CurrencyNameEnum currencyName;

    [SerializeField] Sprite sprite;

    public CurrencyNameEnum CurrencyName => currencyName;
    public Sprite Sprite => sprite;
}