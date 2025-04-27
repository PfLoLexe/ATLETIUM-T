using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ATLETIUM_T.Models;
using ATLETIUM_T.serverDatabase;

namespace ATLETIUM_T.api.repositories;

public class UserRepository
{
    private readonly HttpClientService _service = new HttpClientService();
    
    public async Task<Guid?> GetCurrentUserGuid(Token token)
    {
        try
        {
            var response = await _service.GetAsync<Guid>("/v1/get-current-user-uuid", token);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<CurrentUserInfo?> GetCurrentUserInfo(Token token)
    {
        try
        {
            var response = await _service.GetAsync<CurrentUserInfo>("/v1/user/me", token);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}