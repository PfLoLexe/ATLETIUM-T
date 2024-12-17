using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.localDatabase.controllers;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;

namespace ATLETIUM_T.api.controllers;

public class UserController(UserRepository repository)
{
    private UserRepository _userRepository = repository;
    private readonly LocalSettingsController _localController =
        new LocalSettingsController(new LocalSettingsRepository());

    public async Task<Guid?> GetCurrentUserGuid()
    {

        var token = await _localController.GetToken();
        if (token == null) return null;// TODO: start auth
        
        var userGuid = await _userRepository.GetCurrentUserGuid(token);
        if (userGuid == null)
        {
            new ToastMessage().ShortToast("Ошибка получения пользователя");
            return null;
        }
        
        return userGuid;
    }
    
    public async Task<CurrentUserInfo?> GetCurrentUserInfo()
    {

        var token = await _localController.GetToken();
        if (token == null) return null;// TODO: start auth
        
        var userInfo = await _userRepository.GetCurrentUserInfo(token);
        if (userInfo == null)
        {
            new ToastMessage().ShortToast("Ошибка получения пользователя");
            return null;
        }
        
        return userInfo;
    }
}