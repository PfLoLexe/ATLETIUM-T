using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;

namespace ATLETIUM_T.localDatabase.controllers;

public class LocalAttendanceMarksController(LocalAttendanceMarksRepository repository)
{
    public async Task<int> UpdateMark(Guid trainSpecificId, Client client)
    {
        return await repository.UpdateMark(trainSpecificId, client);
    }

    public async Task<Dictionary<Guid, Client>?> GetMarks(Guid? trainSpecificId)
    {
        return await repository.GetMarks(trainSpecificId);
    }
}