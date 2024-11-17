using System.Threading.Tasks;
using ATLETIUM_T.localDatabase.models;
using GammaNew.LocalDatabase.Models;
using GammaNew.Src.LocalDatabase.Models;
using SQLite;

namespace GammaNew.LocalDatabase;

public class LocalDb
{
    public SQLiteAsyncConnection _db { get; private set; }

    public async Task InitDatabase()
    {
        if (_db is not null)
            return;

        _db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await CreateTables();
    }

    private async Task CreateTables()
    {
        //var result = await _db.CreateTableAsync<User>();
        var result = await _db.CreateTableAsync<AppSettings>();
        await _db.CreateTableAsync<TrainsAttendanceMarks>();
    }
}