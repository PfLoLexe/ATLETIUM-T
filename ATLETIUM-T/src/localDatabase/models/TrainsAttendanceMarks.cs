using System;
using SQLite;

namespace ATLETIUM_T.localDatabase.models;

public class TrainsAttendanceMarks
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public Guid TrainId { get; set; }
    public Guid ClientId { get; set; }
    public int VisitStatus { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }

}