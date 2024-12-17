using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.localDatabase.controllers;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;
using ATLETIUM_T.serverDatabase;

namespace ATLETIUM_T.api.controllers;

public class MessageController(MessageRepository repository)
{
    private MessageRepository _messageRepository = repository;
    private readonly LocalSettingsController _localController =
        new LocalSettingsController(new LocalSettingsRepository());

    public async Task SendMessage(MessageRequest message)
    {

        var token = await _localController.GetToken();
        if (token == null) return;// TODO: start auth
        
        await _messageRepository.SendMessage(message, token);
        // if (userGuid == null)
        // {
        //     new ToastMessage().ShortToast("Ошибка получения пользователя");
        //     return null;
        // }
    }
    
    public async Task<List<MessageResponse>?> GetMessagesListAsync(Guid? dialogue_id)
    {

        var token = await _localController.GetToken();
        if (token == null) return null;// TODO: start auth
        
        var trainMainList = await _messageRepository.GetMessagesListAsync(dialogue_id, token);
        // if (trainMainList == null)
        // {
        //     new ToastMessage().ShortToast("Ошибка получения сообщений");
        //     return null;
        // }
        
        return trainMainList;
    }
}