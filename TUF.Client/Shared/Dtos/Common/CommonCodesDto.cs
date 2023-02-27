using Daniel.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUF.Client.Shared.Dtos.Boards;

namespace TUF.Client.Shared.Dtos.Common;

public class CommonCodesDto : DtoBase<BoardCommentDto.Response, BoardCommentDto.Request>
{
    public class Request
    {
        public readonly ApiMetaData CreateMeta = new ApiMetaData
        {
            Title = "로그인",
            httpmethod = Knus.Common.Services.HttpMethods.POST,
            UrlPath = "/api/tokens/Login"
        };
    }

    public record Response();
}
