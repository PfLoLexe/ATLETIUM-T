﻿using System.Net;
using ATLETIUM_T.Models;
using ATLETIUM_T.serverDatabase;

namespace ATLETIUM_T.api.repositories;

public class TrainRepository
{
    private readonly HttpClientService _service = new HttpClientService();

    public async Task<List<TrainMain>?> GetTrainsListAsync(int week_day_number, DateTime rawDate, Token token)
    {
        try
        {
            string date = rawDate.ToString("yyyy-MM-dd");
            var response = await _service.PostAsync("/train-main/get-list/by-week-day-number",
                new { week_day_number, date }, token);
            if (response.StatusCode != HttpStatusCode.OK) return null;
            
            return await _service.Deserialize<List<TrainMain>>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<TrainSpecific?> GetTrainInfo(Guid? train_main_id, DateTime rawDate)
    {
        try
        {
            string date = rawDate.ToString("yyyy-MM-dd");
            var response = await _service.PostAsync("/train-specific/get",
                new { train_main_id, date });
            if (response.StatusCode != HttpStatusCode.OK) return null;
            
            return await _service.Deserialize<TrainSpecific>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}