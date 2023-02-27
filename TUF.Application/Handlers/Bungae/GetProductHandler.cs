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
using TUF.Domain.OldMarket;

namespace TUF.Application.Handlers.Bungae;

public class GetProductHandler
{
    public class Query : IRequest<Result>
    { 
        public BungaeDto.Request param { get; set; }
        
    }
    public class Result : BaseDtoGeneric<BungaeDto.Response,Query>
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
                //https://api.bunjang.co.kr/api/1/find_v2.json?q=%EB%82%B4%EC%85%94%EB%84%90%EC%A7%80%EC%98%A4%EA%B7%B8%EB%9E%98%ED%94%BD
                var url = "https://api.bunjang.co.kr/api/1/find_v2.json?";
                var query  = "q=" + HttpUtility.UrlEncode(req.param.Keyword);
                var param = "&order=date&page=0&request_id=2023217155326&stat_uid=8560808&stat_device=w&n=100&stat_category_required=1&req_ref=search&version=4";
                var finalurl =new Uri(url + query + param);
                WebClient wc = new();
                var json= await wc.DownloadStringTaskAsync(finalurl);
                dt.OutPutValue = new BungaeDto.Response();
                var contdata = JsonConvert.DeserializeObject<Domain.OldMarket.BungaeModel>(json);
                foreach (var j  in contdata.list)
                {
                    Client.Shared.Bodys.OldMarket.BungaeModel bm = new Client.Shared.Bodys.OldMarket.BungaeModel();
                    bm.Title = j.name;
                    bm.Price = j.price.ToInt().ToComma();
                    bm.product_image = j.product_image;//.Replace("{res}", "500");
                    bm.update_time = j.update_time.ToString();
                    bm.Pid = j.pid;
                    dt.OutPutValue.Products.Add(bm);
                }                

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
