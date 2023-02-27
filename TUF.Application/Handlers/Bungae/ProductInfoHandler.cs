using Knus.Common.Helpers;
using Knus.Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TUF.Client.Shared.Bodys.OldMarket;
using TUF.Client.Shared.Dtos.OldMarket;
using TUF.Database.DbContexts;
using TUF.Domain.OldMarket.Product;

namespace TUF.Application.Handlers.Bungae;

public class ProductInfoHandler
{
    public class Query : IRequest<Result>
    {
        public List<BungaeModel> Products { get; set; } =new List<BungaeModel>();
    }
    public class Result : BaseDtoGeneric<List<BungaeModel>, Query>
    {
    }
    public class Handler : IRequestHandler<Query, Result>
    {
        private readonly ILogger<GetProductHandler> _logger;
        private readonly IApplicationDbContext _Appctx;
        public Handler(ILogger<GetProductHandler> logger,
          IApplicationDbContext Appctx)
        {
            _logger = logger;
            _Appctx = Appctx;
        }
        public async Task<Result> Handle(Query req, CancellationToken cancellationToken)
        {
            Result dt = new Result();
            try
            {
                WebClient wc = new();
                var baseurl = "https://api.bunjang.co.kr/api/pms/v1/products-detail/{0}?viewerUid=-1";
                var imgurl = "";
                foreach (var r in req.Products)
                {
                    try
                    {
                        var callurl = string.Format(baseurl, r.Pid);
                        var json = await wc.DownloadStringTaskAsync(callurl);
                        var contdata = JsonConvert.DeserializeObject<Domain.OldMarket.Product.BungaeProductModel>(json);
                        r.ProductDesc = contdata.data.product.description;
                        r.RegTime = contdata.data.product.updatedBefore;
                        r.ShipInclude = contdata.data.product.includeShippingCost;
                        var proimg = contdata.data.product.imageUrl;
                        var imgcnt = contdata.data.product.imageCount;
                        r.Images = new List<string>();
                        for (int i = 1; i <= imgcnt; i++)
                        {
                            r.Images.Add(proimg.Replace("{cnt}", i.ToString()).Replace("{res}", "1100"));
                        }
                    }
                    catch { }
                }

                //https://api.bunjang.co.kr/api/1/find_v2.json?q=%EB%82%B4%EC%85%94%EB%84%90%EC%A7%80%EC%98%A4%EA%B7%B8%EB%9E%98%ED%94%BD
                //var url = "https://api.bunjang.co.kr/api/1/find_v2.json?";                
                //var finalurl = new Uri(url + query + param);                
                //var json = await wc.DownloadStringTaskAsync(finalurl);
                //dt.OutPutValue = new BungaeDto.Response();
                //var contdata = JsonConvert.DeserializeObject<Domain.OldMarket.BungaeModel>(json);
                //foreach (var j in contdata.list)
                //{
                //    Client.Shared.Bodys.OldMarket.BungaeModel bm = new Client.Shared.Bodys.OldMarket.BungaeModel();
                //    bm.Title = j.name;
                //    bm.Price = j.price.ToInt().ToComma();
                //    bm.product_image = j.product_image;//.Replace("{res}", "500");
                //    bm.update_time = j.update_time.ToString();
                //    dt.OutPutValue.Products.Add(bm);
                //}
                dt.OutPutValue = req.Products;
                dt.Success = true;
            }
            catch (Exception exc)
            {
                dt.Success = false;
                dt.HasError = true;
                dt.Message = exc.Message;
                dt.SystemMessage = ExcetionHelper.ExceptionMessage(exc);
            }
            return dt;
        }
    }

}
