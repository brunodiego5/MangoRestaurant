using Mango.Web.Models;

namespace Mango.Web.Services.IServices;

public interface IBaseService : IDisposable
{
    ResponseProductDto responseModel { get; set; }

    Task<T> SendAsync<T>(ApiRequest apiRequest);
}