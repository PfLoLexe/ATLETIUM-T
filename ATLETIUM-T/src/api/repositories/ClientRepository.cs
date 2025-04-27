using System.Net;
using ATLETIUM_T.Models;
using ATLETIUM_T.serverDatabase;

namespace ATLETIUM_T.api.repositories;

public class ClientRepository
{
    private readonly HttpClientService _service = new HttpClientService();
    
    public async Task<ClientResponse?> GetClientInfo(Guid client_id, Token token)
    {
        try
        { 
            var response = await _service.PostAsync("/v1/client/get-info",
                new {client_id}, token);
            if (response.StatusCode != HttpStatusCode.OK) return null;
            
            return await _service.Deserialize<ClientResponse>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}