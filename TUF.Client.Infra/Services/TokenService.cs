using Blazored.LocalStorage;
using Daniel.Common.Interfaces;
using Knus.Common.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUF.Client.Infra.Common;
using TUF.Client.Shared.Dtos;

namespace TUF.Client.Infra.Services;

public interface ITokenService : ITransient
{
    Task<LoginDto> GetTokenAsync(LoginDto.Request request);
    Task<LoginDto> RefreshAsync(string refreshtoken);

    ValueTask<string> GetLocalToken();
    string SyncGetLocalToken();
}

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly ILocalStorageService _localStorage;
    public TokenService(IConfiguration configuration, ILocalStorageService localStorage)
    {
        _configuration = configuration;
        _localStorage = localStorage;
    }

    public ValueTask<string> GetLocalToken()
    {
        var s = _localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken);
        return s;
    }

    public string SyncGetLocalToken()
    {
        var s = _localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken).GetAwaiter().GetResult();
        return s;
    }

    public async Task<LoginDto> GetTokenAsync(LoginDto.Request request)
    {
        ApiProvider<LoginDto> api = new ApiProvider<LoginDto>();
        api.BaseAddress = _configuration["ApiBaseUrl"];
        api.Apimeta = request.metadata;
        api.SendValue = Newtonsoft.Json.JsonConvert.SerializeObject(request);
        var rt = await api.AsyncCallData();
        if (!rt.Success)
        {
            rt.OutValue.Success = false;
            rt.OutValue.Message = rt.Message;
        }
        return rt.OutValue;
    }

    public async Task<LoginDto> RefreshAsync(string refreshtoken)
    {
        return new LoginDto();
    }

}