using Daniel.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUF.Client.Shared.Dtos.Boards;

namespace TUF.Client.Shared.Dtos.OldMarket;

public class BungaeDto : DtoBase<BungaeDto.Response, BungaeDto.Request>
{
    public static readonly ApiMetaData GetMeta = new ApiMetaData
    {
        Title = "번개장터",
        httpmethod = Knus.Common.Services.HttpMethods.POST,
        UrlPath = "/api/productcollect/bungae"
    };

    public class Request
    {
        [Required(ErrorMessage = "검색어 필수")]
        public string Keyword { get; set; }
        public int Price { get; set; } = 0;
    }

    public class Response
    {
        public List<Bodys.OldMarket.BungaeModel> Products { get; set; } = new List<Bodys.OldMarket.BungaeModel>();   
    }
}
