using ATLETIUM_T.Models;
using Newtonsoft.Json;

namespace ATLETIUM_T.api.repository;

public class TrainRepository(ApiHandler apiHandler)
{
    private ApiHandler _apiHandler = apiHandler;

    public async Task<List<TrainListItem>?> GetTrainsByWeekDayNumber(int dayNumber)
    {
        Uri uri = new Uri($"http://10.0.2.2:9191/get-train-list/by-week-day-number?week_day_number={dayNumber}");
        HttpResponseMessage response = await _apiHandler.Client.GetAsync(uri);
        string jsonString = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<List<TrainListItem>>(jsonString);

    }
}