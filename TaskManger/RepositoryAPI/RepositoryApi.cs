using System.Text;
using System.Text.Json;

namespace TaskManger.RepositoryAPI;

public interface IRepositoryApi
{
    Task<HttpResponseResult<T>> Get<T>(string url);
    Task<HttpResponseResult<TResponse>> Put<TResponse, TRequest>(string url, TRequest send);
    Task<HttpResponseResult<TResponse>> Post<TResponse, TRequest>(string url, TRequest send);    
}


public class RepositoryApi: IRepositoryApi
{
    private readonly HttpClient httpClient;
    public JsonSerializerOptions OptionByDefaultJSON => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
    public RepositoryApi(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        //this.httpClient.Timeout = TimeSpan.FromMinutes(30);
    }

    public async Task<HttpResponseResult<T>> Get<T>(string url)
    {
        var responseHttp = await httpClient.GetAsync(url);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await DeserializeResponse<T>(responseHttp, OptionByDefaultJSON);
            return new HttpResponseResult<T>(response, false, responseHttp);
        }
        else
            return new HttpResponseResult<T>(default, true, responseHttp);
    }

    public async Task<HttpResponseResult<TResponse>> Put<TResponse, TRequest>(string url, TRequest send)
    {
        var sendJson = JsonSerializer.Serialize(send);
        var sendContent = new StringContent(sendJson, Encoding.UTF8, "application/json");
        var responseHttp = await httpClient.PutAsync(url, sendContent);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await DeserializeResponse<TResponse>(responseHttp, OptionByDefaultJSON);
            return new HttpResponseResult<TResponse>(response, false, responseHttp);
        }
        else
            return new HttpResponseResult<TResponse>(default, true, responseHttp);
    }

    public async Task<HttpResponseResult<TResponse>> Post<TResponse, TRequest>(string url, TRequest send)
    {
        var sendJson = JsonSerializer.Serialize(send);
        var sendContent = new StringContent(sendJson, Encoding.UTF8, "application/json");
        var responseHttp = await httpClient.PostAsync(url, sendContent);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await DeserializeResponse<TResponse>(responseHttp, OptionByDefaultJSON);
            return new HttpResponseResult<TResponse>(response, false, responseHttp);
        }
        else
            return new HttpResponseResult<TResponse>(default, true, responseHttp);
    }

    private async Task<T> DeserializeResponse<T>(HttpResponseMessage httpResponseMessage, JsonSerializerOptions jsonSerializerOptions)
    {
        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
    }
}
