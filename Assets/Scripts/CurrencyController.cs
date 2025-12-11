using System;

public class CurrencyController
{
    SaveService saveService;

    public event Action<CurrencyNameEnum> OnCurrencyChanged;

    public CurrencyController(SaveService saveService)
    {
        this.saveService = saveService;
    }

    public int GetAmount(string currencyName)
    {
        return saveService.GetCurrencyAmount(currencyName);
    }


    public void UpdateCurrencyAmount(Reward reward)
    {
        saveService.UpdateCurrencyAmount(reward.Currency.CurrencyName, reward.Quantity);

        OnCurrencyChanged?.Invoke(reward.Currency.CurrencyName);
    }
}