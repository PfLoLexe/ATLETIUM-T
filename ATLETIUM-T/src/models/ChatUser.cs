using System;

namespace ATLETIUM_T.Models;

public class ChatUser
{
    public Guid Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public Guid? ChatId { get; set; }
    
    public string? Fullname => Firstname + " " + Lastname;
}