using System.Security.Claims;
using TUF.Application.Handlers;
using TUF.Client.Shared.Authorization;

namespace TUF.Api.Controllers.Test;


 
public class TestController : VersionNeutralApiController
{
    [HttpGet]
    [AllowAnonymous]
    //[MustHavePermission(TUFAction.View, TUFResource.TEST)]
    public IActionResult Get()
    {
        
        return Ok("ff");
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("t1")]
    //[MustHavePermission(TUFAction.View, TUFResource.TEST)]
    public async Task<IActionResult> Get1()
    {
        TestHandler.Query query = new TestHandler.Query();
        var f= await Mediator.Send(query);
        return Ok(f.OutPutValue);
    }

    [HttpGet]    
    [Route("t2")]
    //[MustHavePermission(TUFAction.View, TUFResource.TEST)]
    public async Task<IActionResult> Get2()
    {
        
        
        return Ok(User.GetUserId());
    }


}
