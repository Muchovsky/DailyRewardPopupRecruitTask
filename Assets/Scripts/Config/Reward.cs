using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Reward
{
    [FormerlySerializedAs("reward")] [SerializeField] CurrencyDefinition currency;
    [SerializeField] int quantity;

    public CurrencyDefinition Currency => currency;
    public int Quantity => quantity;
    
}