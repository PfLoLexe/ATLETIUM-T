using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATLETIUM_T.localDatabase.models;
using ATLETIUM_T.Models;
using GammaNew.LocalDatabase;
using GammaNew.LocalDatabase.Models;
using SQLite;

namespace ATLETIUM_T.localDatabase.repositories;

public class LocalAttendanceMarksRepository
{
    private LocalDb _localDb = new LocalDb();
    private SQLiteAsyncConnection _dbContext;
    
    private async Task InitDatabase()
    {
        await _localDb.InitDatabase();
        _dbContext = _localDb._db;
    }

    public async Task<TrainsAttendanceMarks> GetMark(Guid trainSpecificId, Guid clientId)
    {
        await InitDatabase();

        return await _dbContext.Table<TrainsAttendanceMarks>()
            .Where(i => i.TrainId == trainSpecificId && i.ClientId == clientId).FirstOrDefaultAsync();
    }
    
    public async Task<TrainsAttendanceMarks> GetMark(int id)
    {
        await InitDatabase();

        return await _dbContext.Table<TrainsAttendanceMarks>()
            .Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> UpdateMark(Guid trainSpecificId, Client client)
    {
        await InitDatabase();
        TrainsAttendanceMarks mark = await GetMark(trainSpecificId, client.id);
        TrainsAttendanceMarks newMark = new TrainsAttendanceMarks()
        {
            Id = 0,
            ClientId = client.id,
            TrainId = trainSpecificId,
            VisitStatus = client.visit_status,
            Firstname = client.firstname,
            Lastname = client.lastname
        };

        if (mark != null) newMark.Id = mark.Id;
        if (newMark.Id != 0) return await _dbContext.UpdateAsync(newMark);
        return await _dbContext.InsertAsync(newMark);
        
    }

    public async Task<Dictionary<Guid, Client>?> GetMarks(Guid? trainSpecificId)
    {
        await InitDatabase();
        List<TrainsAttendanceMarks> marks = await _dbContext.Table<TrainsAttendanceMarks>()
            .Where(i => i.TrainId == trainSpecificId).ToListAsync();
        
        if (marks == null) return null;

        Dictionary<Guid, Client> clients = new Dictionary<Guid, Client>();
        foreach (TrainsAttendanceMarks mark in marks)
        {
            clients.Add(mark.ClientId, new Client()
            {
                id = mark.ClientId,
                visit_status = mark.VisitStatus,
                firstname = mark.Firstname,
                lastname = mark.Lastname
            });
        }
        
        return clients;
    }
}