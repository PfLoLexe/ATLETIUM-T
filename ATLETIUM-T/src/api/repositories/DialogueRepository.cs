using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ATLETIUM_T.Models;
using ATLETIUM_T.serverDatabase;

namespace ATLETIUM_T.api.repositories;

public class DialogueRepository
{
    private readonly HttpClientService _service = new HttpClientService();
    
    public async Task<List<DialogueResponse>?> GetActiveDialogueListAsync(Token token)
    {
        try
        {
            var response = await _service.PostAsync("/v1/dialogue/get_list",
                new { }, token);
            if (response.StatusCode != HttpStatusCode.OK) return null;
            
            return await _service.Deserialize<List<DialogueResponse>>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<List<PossibleDialogueResponse>?> GetPossibleDialogueListAsync(Token token)
    {
        try
        {
            const string search_string = "string";
            var response = await _service.PostAsync("/v1/dialogue/possible/get_list",
                new {search_string}, token);
            if (response.StatusCode != HttpStatusCode.OK) return null;
            
            return await _service.Deserialize<List<PossibleDialogueResponse>>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<AddDialogueResponse?> AddDialogueAsync(Guid second_user_id, Token token)
    {
        try
        {
            var response = await _service.PostAsync("/v1/dialogue/add",
                new {second_user_id}, token);
            if (response.StatusCode != HttpStatusCode.OK) return null;
            
            return await _service.Deserialize<AddDialogueResponse>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
       
    
}