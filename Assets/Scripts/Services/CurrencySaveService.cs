public class CurrencySaveService
{
    SaveService saveService;

    public CurrencySaveService(SaveService saveService)
    {
        this.saveService = saveService;
    }

    public int Load(string currencyName)
    {
        return saveService.GetInt(currencyName, 0);
    }

    public void Save(string currencyName, int value)
    {
        saveService.SetInt(currencyName, value);
        saveService.Save();
    }

    public void AddCurrencyAmount(CurrencyNameEnum currencyNameEnum, int increaseBy)
    {
        string currencyName = currencyNameEnum.ToString();
        var currentAmount = saveService.GetInt(currencyName, 0);
        var newAmount = currentAmount + increaseBy;
        saveService.SetInt(currencyName, newAmount);
    }
}