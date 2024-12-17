using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.localDatabase.controllers;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;

namespace ATLETIUM_T.api.controllers;

public class DialogueController(DialogueRepository repository)
{
    private DialogueRepository _dialogueRepository = repository;
    private readonly LocalSettingsController _localController =
        new LocalSettingsController(new LocalSettingsRepository());
    
    public async Task<List<DialogueResponse>?> GetActiveDialogueListAsync()
    {
        var token = await _localController.GetToken();
        if (token == null) return null;// TODO: start auth
        
        var dialogueList = await _dialogueRepository.GetActiveDialogueListAsync(token);
        
        if (dialogueList == null)
        {
            new ToastMessage().ShortToast("Ошибка получения чатов");
            return null;
        }

        return dialogueList;
    }
    
    public async Task<List<PossibleDialogueResponse>?> GetPossibleDialogueListAsync()
    {
        var token = await _localController.GetToken();
        if (token == null) return null;// TODO: start auth
        
        var dialogueList = await _dialogueRepository.GetPossibleDialogueListAsync(token);
        
        if (dialogueList == null)
        {
            new ToastMessage().ShortToast("Ошибка получения чатов");
            return null;
        }

        return dialogueList;
    }
    
    public async Task<AddDialogueResponse?> AddDialogueAsync(Guid second_user_id)
    {
        var token = await _localController.GetToken();
        if (token == null) return null;// TODO: start auth
        
        var dialogueId = await _dialogueRepository.AddDialogueAsync(second_user_id, token);
        
        if (dialogueId == null)
        {
            new ToastMessage().ShortToast("Ошибка создания чата");
            return null;
        }

        return dialogueId;
    }
}