using System;

namespace ATLETIUM_T.Models;

public class MessageResponse
{
    public Guid? id { get; set; }
    public Guid? sender_user_id { get; set; }
    public Guid? recipient_user_id { get; set; }
    public bool is_read { get; set; }
    public string text { get; set; }
    public DateTime send_date { get; set; }
    public bool my_message { get; set; }
}