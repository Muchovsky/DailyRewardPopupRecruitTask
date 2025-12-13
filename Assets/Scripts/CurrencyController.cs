public class CurrencyController
{
    CurrencySaveService currencySaveService;
    CurrencyUI currencyUI;
    CurrencyDefinition currencyDefinition;


    public CurrencyController(CurrencySaveService currencySaveService, CurrencyUI currencyUI, CurrencyDefinition currencyDefinition, EventBus eventBus)
    {
        this.currencySaveService = currencySaveService;
        this.currencyUI = currencyUI;
        this.currencyDefinition = currencyDefinition;

        var currentAmount = GetAmount(currencyDefinition.CurrencyName.ToString());
        this.currencyUI.Init(currencyDefinition.Sprite, currentAmount.ToString());
        eventBus.OnRewardClaimed += OnRewardClaimed;
    }

    int GetAmount(string currencyName)
    {
        return currencySaveService.Load(currencyName);
    }

    void OnRewardClaimed(RewardDefinition rewardDefinition)
    {
        if (rewardDefinition.Currency.CurrencyName != currencyDefinition.CurrencyName)
            return;

        AddCurrency(rewardDefinition.Quantity);
    }


    void AddCurrency(int amount)
    {
        currencySaveService.AddCurrencyAmount(currencyDefinition.CurrencyName, amount);
        currencyUI.UpdateAmount(GetAmount(currencyDefinition.CurrencyName.ToString()));
    }
}