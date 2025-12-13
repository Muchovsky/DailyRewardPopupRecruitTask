using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class RewardDefinition
{
    [SerializeField] CurrencyDefinition currency;
    [SerializeField] int quantity;

    public CurrencyDefinition Currency => currency;
    public int Quantity => quantity;
}