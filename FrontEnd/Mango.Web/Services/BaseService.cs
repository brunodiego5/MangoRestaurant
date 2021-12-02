using Mango.Web.Models;
using Mango.Web.Services.IServices;
using System.Text;
using System.Text.Json;

namespace Mango.Web.Services;

public class BaseService : IBaseService
{
    public ResponseProductDto responseModel { get; set; }

    public IHttpClientFactory _httpClientFactory { get; set; }
    public ILogger<BaseService> _logger { get; set; }
    private readonly JsonSerializerOptions _jsonOptions;

    public BaseService(IHttpClientFactory httpClientFactory, ILogger<BaseService> logger)
    {
        responseModel = new ResponseProductDto();
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _jsonOptions = new(JsonSerializerDefaults.Web);
    }

    public async Task<T> SendAsync<T>(ApiRequest apiRequest)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("MangoAPI");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accpet", "application/json");
            message.RequestUri = new Uri(apiRequest.Url);

            client.DefaultRequestHeaders.Clear();

            if (apiRequest.Data != null)
            {
                message.Content = new StringContent(JsonSerializer.Serialize(apiRequest.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage apiResponse = null;
            switch (apiRequest.ApiType)
            {
                case StaticDetails.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;

                case StaticDetails.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;

                case StaticDetails.ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;

                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await client.SendAsync(message);
            
            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var apiResponseDto = JsonSerializer.Deserialize<T>(apiContent, _jsonOptions);

            return apiResponseDto;
        }
        catch (Exception exception)
        {
            _logger.LogError($"ERROR: An exception was thrown while calling SendAsync(): {exception.Message} | {exception.StackTrace}");

            var errorDto = new ResponseProductDto();
            errorDto.IsSuccess = false;
            errorDto.DisplayMessage = "ERROR";
            errorDto.ErrorMessages = new List<string>() { $"An exception was thrown while calling SendAsync(): {exception.Message}" };

            var response = JsonSerializer.Serialize(errorDto);
            var apiResponseDto = JsonSerializer.Deserialize<T>(response);

            return apiResponseDto;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}