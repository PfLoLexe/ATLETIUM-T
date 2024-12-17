using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ATLETIUM_T.Models;

namespace ATLETIUM_T.serverDatabase;

public class HttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService()
    {
        _httpClient = new HttpClient();
        // Можно настроить базовый адрес для всех запросов
        _httpClient.BaseAddress = new Uri("http://10.0.2.2:8000");
    }

    // Метод для GET-запросов
    public async Task<T?> GetAsync<T>(string endpoint)
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();  // выбросит исключение при ошибке

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content);
    }
    
    public async Task<T?> GetAsync<T>(string endpoint, Token token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token.access_token);
        
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();  // выбросит исключение при ошибке

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content);
    }

    // Метод для POST-запросов
    public async Task<HttpResponseMessage> PostAsync(string endpoint, object body)
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);
        // response.EnsureSuccessStatusCode();

        return response;
    }
    
    public async Task<HttpResponseMessage> PostAsync(string endpoint, object body, Token token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token.access_token);
        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);
        // response.EnsureSuccessStatusCode();

        return response;
    }
    
    // Метод для PUT-запросов
    public async Task<HttpResponseMessage> PutAsync(string endpoint, object body)
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(endpoint, content);
        // response.EnsureSuccessStatusCode();

        return response;
    }
    
    public async Task<HttpResponseMessage> PutAsync(string endpoint, object body, Token token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token.access_token);
        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(endpoint, content);
        // response.EnsureSuccessStatusCode();

        return response;
    }

    public async Task<T?> Deserialize<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(responseContent);
        return result;
    }

    public async Task<bool> IsTokenExpired()
    {
        return true;
    }
}