using System;
using System.Net;
using System.Threading.Tasks;
using ATLETIUM_T.Models;
using ATLETIUM_T.serverDatabase;

namespace ATLETIUM_T.api.repositories;

public class AuthorizationRepository
{
    private readonly HttpClientService _service = new HttpClientService();

    public async Task<Token?> AuthorizeAsync(string username, string password)
    {
        try
        {
            var response = await _service.PostAsync("/token",
                new { username, password });
            if (response.StatusCode != HttpStatusCode.OK) return null;
            
            return await _service.Deserialize<Token>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<PinCodeResponse?> AuthorizeByPinCodeAsync(string pincode, Token token)
    {
        try
        {
            var response = await _service.PostAsync("/verify-pincode",
                new { pincode }, token);
            
            return await _service.Deserialize<PinCodeResponse>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}