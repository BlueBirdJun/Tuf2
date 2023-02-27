using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TUF.Api.Controllers;


[Route("api/v{version:apiVersion}/[controller]")]
//[Route("api/[controller]")]
public class VersionedApiController : BaseApiController
{
}
