using System.Threading.Tasks;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.localDatabase.controllers;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;

namespace ATLETIUM_T.api.controllers;

public class AuthorizationController(AuthorizationRepository authorizationRepository)
{
    private AuthorizationRepository _authorizationRepository = authorizationRepository;
    private readonly LocalSettingsController _localController =
        new LocalSettingsController(new LocalSettingsRepository());

    public async Task<Token?> AuthorizeAsync(string username, string password)
    {

            var token = await _authorizationRepository.AuthorizeAsync(username, password);
            if (token == null)
            {
                new ToastMessage().ShortToast("Неверное имя пользователя или пароль");
                return null;
            }

            new ToastMessage().ShortToast("Успешная авторизация");
            return token;
    }
    
    public async Task<PinCodeResponse?> AuthorizeByPinCodeAsync(string pincode)
    {
        var token = await _localController.GetToken();

        if (token == null) return null;// TODO: start auth
        
        var pinCodeStatus = await _authorizationRepository.AuthorizeByPinCodeAsync(pincode, token);
        return pinCodeStatus;
    }
}