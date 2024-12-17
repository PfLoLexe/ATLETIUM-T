using System;

namespace ATLETIUM_T.Models;

public class MessageRequest
{
    public Guid? recipient_user_id { get; set; }
    public Guid? dialogue_id { get; set; }
    public string? text { get; set; }
    public DateTime send_date { get; set; }
}