using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUF.Client.Infra.Auth;

public class AccessTokenProviderAccessor : IAccessTokenProviderAccessor
{
    private readonly IServiceProvider _provider;
    private IAccessTokenProvider? _tokenProvider;

    public AccessTokenProviderAccessor(IServiceProvider provider) =>
        _provider = provider;

    public IAccessTokenProvider TokenProvider =>
        _tokenProvider ??= _provider.GetRequiredService<IAccessTokenProvider>();
}