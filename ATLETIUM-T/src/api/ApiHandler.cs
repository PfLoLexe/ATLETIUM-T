namespace ATLETIUM_T.api;

public class ApiHandler
{
    private static HttpClient _client = new HttpClient();
    public HttpClient Client => _client;

    private string BaseUrl => "http://10.0.2.2:";
    public string TrainsUrl => $"{BaseUrl}9191";

    // public async Task<HttpResponseMessage> Get(Uri uri, object parameters = null)
    // {
    //     // if (parameters == null) HttpResponseMessage response = await _client.GetAsync(uri);
    //     //
    //     //
    //     //
    //     // // string content = await response.Content.ReadAsStringAsync();
    //     // return response;
    // }
}