using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyAmount;
    [SerializeField] Image currencyImage;
    CurrencyController currencyController;
    CurrencyDefinition currencyDefinition;


    public void Construct(CurrencyController controller, CurrencyDefinition currency)
    {
        currencyController = controller;
        currencyDefinition = currency;
        currencyController.OnCurrencyChanged += UpdateView;
    }

    public void Init()
    {
        UpdateView(currencyDefinition.CurrencyName);
        currencyImage.sprite = currencyDefinition.Sprite;
    }

    void UpdateView(CurrencyNameEnum currency)
    {
        if (currency == currencyDefinition.CurrencyName)
        {
            currencyAmount.text = currencyController.GetAmount(currency.ToString()).ToString();
        }
    }
}