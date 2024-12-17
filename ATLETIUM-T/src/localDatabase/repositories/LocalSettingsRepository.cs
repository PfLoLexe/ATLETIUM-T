using System.Threading.Tasks;
using ATLETIUM_T.Models;
using GammaNew.LocalDatabase;
using GammaNew.LocalDatabase.Models;
using SQLite;

namespace ATLETIUM_T.localDatabase.repositories;

public class LocalSettingsRepository
{
    private LocalDb _localDb = new LocalDb();
    private SQLiteAsyncConnection _dbContext;

    private async Task InitDatabase()
    {
        await _localDb.InitDatabase();
        _dbContext = _localDb._db;
    }

    public async Task<int> SaveToken(string? token, string? tokenType)
    {
        await InitDatabase();

        var oldSettings = await GetSettings(1);

        
        AppSettings appSettings = new AppSettings()
        {
            Id = 0,
            Token = token,
            TokenType = tokenType
        };
        
        if (oldSettings != null) appSettings.Id = oldSettings.Id;
        if (appSettings.Id != 0) return await _dbContext.UpdateAsync(appSettings);
        return await _dbContext.InsertAsync(appSettings);
    }

    public async Task<Token?> GetToken()
    {
        await InitDatabase();
        
        AppSettings settings = await _dbContext.Table<AppSettings>()
            .Where(i => i.Id == 1).FirstOrDefaultAsync();
        if (settings == null) return null;
        if (settings.Token == null) return null;
        
        return new Token(settings.Token, settings.TokenType);
    }

    public async Task<int> DeleteToken()
    {
        await InitDatabase();

        var oldSettings = await GetSettings(1);

        
        AppSettings appSettings = new AppSettings()
        {
            Id = 0,
            Token = null,
            TokenType = null
        };
        
        if (oldSettings != null) appSettings.Id = oldSettings.Id;
        if (appSettings.Id != 0) return await _dbContext.UpdateAsync(appSettings);
        return await _dbContext.InsertAsync(appSettings);
    }

    private async Task<AppSettings> GetSettings(int id)
    {
        await InitDatabase();
        
        AppSettings settings = await _dbContext.Table<AppSettings>()
            .Where(i => i.Id == id).FirstOrDefaultAsync();

        return settings;
    }
}
