﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUF.Api.Infra.Identity.Tokens;

public record RefreshTokenRequest(string Token, string RefreshToken);