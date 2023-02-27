﻿using Daniel.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUF.Client.Shared.Bodys.Boards;

namespace TUF.Client.Shared.Dtos.Boards;

public class BoardDto : DtoBase<BoardDto.Response, BoardCommentDto.Request>
{
    public class Request: BoardMeta.Body
    {
        
    }

    public class Response  { } 
}


