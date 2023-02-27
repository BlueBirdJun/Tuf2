﻿using Daniel.Common.Interfaces;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TUF.Database.DbContexts;
using TUF.Database.Identity.Models;
using TUF.Api.Infra.Auth;

namespace TUF.Api.Infra.Identity.Users;

internal partial class UserService : IUserService
{
}
