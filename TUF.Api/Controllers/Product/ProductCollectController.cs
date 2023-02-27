using Microsoft.AspNetCore.Http.HttpResults;
using TUF.Application.Handlers.Bungae;
using TUF.Client.Shared.Dtos.Boards;
using TUF.Client.Shared.Dtos.OldMarket;

namespace TUF.Api.Controllers.Product;

public class ProductCollectController : VersionNeutralApiController
{
    [Route("Bungae")]
    [AllowAnonymous]
    public async  Task<BungaeDto> GetBungae(BungaeDto.Request param)
    {
        BungaeDto crt = new BungaeDto();
        GetProductHandler.Query query = new GetProductHandler.Query();
        query.param = param;
        var  rt =await Mediator.Send(query);
        crt.Success= rt.Success;
        crt.OutPutValue = rt.OutPutValue;

        ProductInfoHandler.Query getparam= new ProductInfoHandler.Query();
        getparam.Products = crt.OutPutValue.Products;
        var prt = await Mediator.Send(getparam);
        if(prt.Success)
        {
            crt.OutPutValue.Products = prt.OutPutValue;
        }
        return crt;
    }
}
