﻿using NSwag.Annotations;
using System.Threading;
using TUF.Api.Infra.Identity.Tokens;
using TUF.Client.Shared.Common.Exceptions;
using TUF.Client.Shared.Dtos;

namespace TUF.Api.Controllers.Identity;

public class TokensController : VersionNeutralApiController
{
    private readonly ITokenService _tokenService;

    public TokensController(ITokenService tokenService) => _tokenService = tokenService;

    //[HttpGet]
    //[Route("Login")]
    //[AllowAnonymous]
    //public ActionResult<UserSession> Login()
    //{
    //    var jwtAuthenticationManager = new JwtAuthernticationManager();
    //    var userSession = jwtAuthenticationManager.GenerateJwtToken("","");
    //    if (userSession is null)
    //        return Unauthorized();
    //    else
    //        return userSession;

    //}

   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [OpenApiOperation("Request an access token using credentials.", "")]    
    public Task<TokenResponse> GetTokenAsync(TokenRequest request, CancellationToken cancellationToken)
    {
        return _tokenService.GetTokenAsync(request, GetIpAddress(), cancellationToken);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Login")]
    public async Task<LoginDto> LoginToken([FromBody] LoginDto.Request data)
    {
        LoginDto rt = new();
        try
        { 
            var rt1 = await _tokenService.LoginTokenAsync(data, GetIpAddress());
            rt.Success = true;
            rt.Message = "성공";
            rt.OutPutValue = new LoginDto.Response(rt1.Token, rt1.RefreshToken, rt1.RefreshTokenExpiryTime); 
        }
        catch (UnauthorizedException uex)
        {
            switch(uex.Message)
            {
                case "Authentication Failed.":
                    rt.Message = "계정정보꽝";
                    break;
                case "User Not Active. Please contact the administrator.":
                    rt.Message = "관리자가 승인안함";
                    break;
                case "E-Mail not confirmed.":
                    rt.Message = "email 승인안남";
                    break;
            }
        }  
        return rt;
    }


    [HttpPost("refresh")]
    [AllowAnonymous]

    [OpenApiOperation("Request an access token using a refresh token.", "")]
    [ApiConventionMethod(typeof(TUFApiConventions), nameof(TUFApiConventions.Search))]
    public Task<TokenResponse> RefreshAsync(RefreshTokenRequest request)
    {
        return _tokenService.RefreshTokenAsync(request, GetIpAddress());
    }
   

    private string GetIpAddress() =>
    Request.Headers.ContainsKey("X-Forwarded-For")
        ? Request.Headers["X-Forwarded-For"]
        : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";


     
}
