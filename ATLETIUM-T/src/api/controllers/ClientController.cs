using ATLETIUM_T.api.repositories;
using ATLETIUM_T.localDatabase.controllers;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;

namespace ATLETIUM_T.api.controllers;

public class ClientController(ClientRepository repository)
{
    private ClientRepository _clientRepository = repository;
    private readonly LocalSettingsController _localController =
        new LocalSettingsController(new LocalSettingsRepository());
    
    public async Task<ClientResponse?> GetClientInfo(Guid client_id)
    {

        var token = await _localController.GetToken();
        if (token == null) return null;// TODO: start auth
        
        var clientInfo = await _clientRepository.GetClientInfo(client_id, token);
        if (clientInfo == null)
        {
            new ToastMessage().ShortToast("Ошибка получения информации");
            return null;
        }
        
        return clientInfo;
    }
}