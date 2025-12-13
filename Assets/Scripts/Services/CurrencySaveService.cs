public class CurrencySaveService
{
    SaveService saveService;

    public CurrencySaveService(SaveService saveService)
    {
        this.saveService = saveService;
    }

    public int Load(string currencyName)
    {
        return saveService.GetInt(currencyName);
    }

    void Save(string currencyName, int value)
    {
        saveService.SetInt(currencyName, value);
        saveService.Save();
    }

    public void AddCurrencyAmount(CurrencyNameEnum currencyNameEnum, int increaseBy)
    {
        var currencyName = currencyNameEnum.ToString();
        var currentAmount = Load(currencyName);
        var newAmount = currentAmount + increaseBy;
        Save(currencyName, newAmount);
    }
}