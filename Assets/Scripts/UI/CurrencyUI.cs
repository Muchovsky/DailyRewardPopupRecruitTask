using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyAmount;
    [SerializeField] Image currencyImage;

    CurrencyController currencyController;

    public void Construct(CurrencyController controller)
    {
        currencyController = controller;
    }

    public void Init(Sprite sprite, string amount)
    {
        currencyImage.sprite = sprite;
        currencyAmount.text = amount;
    }

    public void UpdateAmount(int amount)
    {
        currencyAmount.text = amount.ToString();
    }
}