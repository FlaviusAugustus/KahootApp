using DefaultNamespace;
using Microsoft.Extensions.Options;

namespace KahootFrontend.Services;

public class ApiService
{
    public HttpClient Client { get; } 
    private IOptions<HttpClientOptions> _options;
    
    public ApiService(HttpClient httpClient, IOptions<HttpClientOptions> options)
    {
        Client = httpClient;
        _options = options;
        
        Client.BaseAddress = new Uri(_options.Value.ApiBaseUrl);
    }
}