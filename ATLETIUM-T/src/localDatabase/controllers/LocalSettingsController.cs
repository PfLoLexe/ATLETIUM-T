using System.Threading.Tasks;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;

namespace ATLETIUM_T.localDatabase.controllers;

public class LocalSettingsController(LocalSettingsRepository repository)
{
    public async Task<int> SaveToken(string? token, string? tokenType)
    {
        return await repository.SaveToken(token, tokenType);
    }

    public async Task<Token?> GetToken()
    {
        return await repository.GetToken();
    }
}
