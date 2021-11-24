
using Mango.Web.Models;
using System;

namespace Mango.Web.Services.IServices;

public interface IBaseService : IDisposable
{
    ResponseProductDto responseModel { get; set; }
    Task<T> SendAsync<T>(ApiRequest apiRequest);
}