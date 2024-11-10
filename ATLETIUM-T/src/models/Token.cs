namespace ATLETIUM_T.Models;

public class Token(string access_token, string token_type)
{
    public string? access_token { get; set; } = access_token;
    public string? token_type { get; set; } = token_type;
}