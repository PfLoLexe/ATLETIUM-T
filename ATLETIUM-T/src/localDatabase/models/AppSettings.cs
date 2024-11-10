using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaNew.LocalDatabase.Models;

public class AppSettings
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Token { get; set; }
    public string? TokenType { get; set; }
}
