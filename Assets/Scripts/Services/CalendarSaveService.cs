public class CalendarSaveService
{
    SaveService saveService;

    public CalendarSaveService(SaveService saveService)
    {
        this.saveService = saveService;
    }

    string StartDayKey(string id) => id + "_START";
    string ClaimedKey(string id) => id + "_CLAIMED";
    string LastDayKey(string id) => id + "_LAST";


    public CalendarData Load(string calendarID)
    {
        var cs = new CalendarData
        {
            calendarID = calendarID,
            startDate = saveService.GetString(StartDayKey(calendarID)),
            lastClaimDate = saveService.GetString(LastDayKey(calendarID))
        };
        var claimedKeys = saveService.GetString(ClaimedKey(calendarID));
        if (!string.IsNullOrEmpty(claimedKeys))
        {
            var keys = claimedKeys.Split(',');
            foreach (var key in keys)
                if (int.TryParse(key, out var day))
                    cs.claimedDays.Add(day);
        }

        cs.claimedDays.Sort(); // just in case
        return cs;
    }

    public void Save(CalendarData calendarData)
    {
        saveService.SetString(StartDayKey(calendarData.calendarID), calendarData.startDate);
        saveService.SetString(LastDayKey(calendarData.calendarID), calendarData.lastClaimDate);
        saveService.SetString(ClaimedKey(calendarData.calendarID), string.Join(",", calendarData.claimedDays));
        saveService.Save();
    }
}