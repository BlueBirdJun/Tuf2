using Knus.Common.Helpers;
using Knus.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUF.Database.DbContexts;

namespace TUF.Application.Handlers;

public class TestHandler
{
    public class Query : IRequest<Result>
    {
    }
    public class Result : BaseDto
    {
    }

    public class Handler : IRequestHandler<Query, Result>
    {
        private readonly ILogger<TestHandler> _logger;
        private readonly IApplicationDbContext _Appctx;
        public Handler(ILogger<TestHandler> logger,
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
                dt.OutPutValue = _Appctx.BoardInfo.Count();
                 
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
