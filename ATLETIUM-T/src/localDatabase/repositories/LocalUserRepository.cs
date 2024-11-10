// using GammaNew.LocalDatabase;
// using GammaNew.Src.LocalDatabase.Models;
// using SQLite;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace GammaNew.Src.LocalDatabase.Repositories;
//
// class LocalUserRepository
// {
//     private LocalDb _localDb = new LocalDb();
//     private SQLiteAsyncConnection _dbContext;
//
//     private async Task InitDatabase()
//     {
//         await _localDb.InitDatabase();
//         _dbContext = _localDb._db;
//     }
//
//     public async Task<User> GetUserById(Guid userId)
//     {
//         await InitDatabase();
//
//         var stringGuid = userId.ToString();
//         return await _dbContext.Table<User>().Where(
//             i => i.UserGuidId == stringGuid)
//             .FirstOrDefaultAsync();
//     }
//
//     public async Task<int> CreateUser(User user)
//     {
//         if (user.Id != 0) return 0;
//
//         return await _dbContext.InsertAsync(user);
//     }
//
//     public async Task<int> UpdateUserTimerSettings(Guid userId, int timeInSeconds)
//     {
//         await InitDatabase();
//
//         var user = await GetUserById(userId);
//         user.UpdatingTimerInSec = timeInSeconds;
//
//         return await _dbContext.UpdateAsync(user);
//     }
// }
