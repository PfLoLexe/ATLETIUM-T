﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.localDatabase.controllers;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;

namespace ATLETIUM_T.api.controllers;

public class TrainController(TrainRepository repository)
{
    private TrainRepository _trainRepository = repository;
    private readonly LocalSettingsController _localController =
        new LocalSettingsController(new LocalSettingsRepository());

    public async Task<List<TrainMain>?> GetTrainsListAsync(int week_day_number, DateTime date)
    {

        var token = await _localController.GetToken();

        if (token == null) return null;// TODO: start auth
        
        var trainMainList = await _trainRepository.GetTrainsListAsync(week_day_number, date, token);
        if (trainMainList == null)
        {
            new ToastMessage().ShortToast("Ошибка получения тренировок");
            return null;
        }
        
        return trainMainList;
    }
    
    public async Task<TrainSpecific> GetTrainInfo(Guid? train_main_id, DateTime date)
    {
        var train = await _trainRepository.GetTrainInfo(train_main_id, date);
        if (train == null)
        {
            new ToastMessage().ShortToast("Ошибка получения тренировоки");
            return null;
        }
        
        return train;
    }
}