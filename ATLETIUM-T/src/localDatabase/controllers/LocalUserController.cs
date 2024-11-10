// using GammaNew.Src.LocalDatabase.Models;
// using GammaNew.Src.LocalDatabase.Repositories;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace GammaNew.Src.LocalDatabase.Controllers;
//
// class LocalUserController(LocalUserRepository repository)
// {
//     public async Task<int> GetUserTimerSettingsInSec(Guid userId)
//     {
//         var user = await repository.GetUserById(userId);
//         if (user == null)
//         {
//             return 0;
//         }
//
//         return user.UpdatingTimerInSec;
//     }
//
//     public async Task<int> UpdateUserTimerSettings(Guid userId, int timeInSeconds)
//     {
//         if (timeInSeconds < 1)
//         {
//             return 0;
//         }
//
//         return await repository.UpdateUserTimerSettings(userId, timeInSeconds);
//     }
//
//     /// <summary>
//     /// Authorize user, if there is a new user, then creates his data in the local database
//     /// </summary>
//     /// <param name="userId">Person guid id</param>
//     /// <returns>Amount of updated rows</returns>
//     public async Task<int> AuthorizeUser(Guid userId)
//     {
//         var user = await repository.GetUserById(userId);
//         if (user != null)
//         {
//             // TODO: Add authorization handling
//             return 0;
//         }
//
//         var newUser = new User
//         {
//             Id = 0,
//             LastLoginDate = DateTime.Now,
//             UserGuidId = userId.ToString(),
//             Username = null,
//             IsLastLoggedIn = true,
//             UpdatingTimerInSec = 30
//         };
//
//         return await repository.CreateUser(newUser);
//     }
//
// }
