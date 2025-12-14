using UnityEngine;

public class SaveService
{
    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public int GetInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key);
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }
}