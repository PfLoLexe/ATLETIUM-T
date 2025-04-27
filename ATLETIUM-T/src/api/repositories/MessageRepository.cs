using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ATLETIUM_T.Models;
using ATLETIUM_T.serverDatabase;

namespace ATLETIUM_T.api.repositories;

public class MessageRepository
{
    private readonly HttpClientService _service = new HttpClientService();
    
    public async Task SendMessage(MessageRequest message, Token token)
    {
        try
        { 
            var response = await _service.PostAsync("/v1/message/add",
                message, token);
            if (response.StatusCode != HttpStatusCode.OK) return;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }
    }

    public async Task<List<MessageResponse>?> GetMessagesListAsync(Guid? dialogue_id, Token token)
    {
        try
        { 
            var response = await _service.PostAsync("/v1/message/get_list", new {dialogue_id}, token);
            if (response.StatusCode != HttpStatusCode.OK) return null;
            
            return await _service.Deserialize<List<MessageResponse>>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}