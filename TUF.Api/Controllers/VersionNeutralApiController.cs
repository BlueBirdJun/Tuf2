using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace TUF.Api.Controllers;

[Route("api/[controller]")]
[ApiVersionNeutral]

public class VersionNeutralApiController : BaseApiController
{
}