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
using Microsoft.AspNetCore.WebUtilities;
using TUF.Api.Infra.Common;
using Microsoft.EntityFrameworkCore;
using TUF.Client.Shared.Common.Exceptions;

namespace TUF.Api.Infra.Identity.Users;

internal partial class UserService : IUserService
{
    public async Task<string> GetEmailVerificationUriAsync(ApplicationUser user, string origin)
	{
		string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
		const string route = "api/users/confirm-email/";
		var endpointUri = new Uri(string.Concat($"{origin}/", route));
		string verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), QueryStringKeys.UserId, user.Id);
		verificationUri = QueryHelpers.AddQueryString(verificationUri, QueryStringKeys.Code, code);
		//verificationUri = QueryHelpers.AddQueryString(verificationUri, MultitenancyConstants.TenantIdName, _currentTenant.Id!);
		return verificationUri;
	}

	public async Task<string> ConfirmEmailAsync(string userId, string code, string tenant, CancellationToken cancellationToken)
	{
		var user = await _userManager.Users
			.Where(u => u.Id == userId && !u.EmailConfirmed)
			.FirstOrDefaultAsync(cancellationToken);

		_ = user ?? throw new InternalServerException("An error occurred while confirming E-Mail.");

		code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
		var result = await _userManager.ConfirmEmailAsync(user, code);

		return result.Succeeded
			? string.Format("Account Confirmed for E-Mail {0}. You can now use the /api/tokens endpoint to generate JWT.", user.Email)
			: throw new InternalServerException(string.Format("An error occurred while confirming {0}", user.Email));
	}

	public async Task<string> ConfirmPhoneNumberAsync(string userId, string code)
	{
		var user = await _userManager.FindByIdAsync(userId);

		_ = user ?? throw new InternalServerException("An error occurred while confirming Mobile Phone.");

		var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, code);

		return result.Succeeded
			? user.EmailConfirmed
				? string.Format("Account Confirmed for Phone Number {0}. You can now use the /api/tokens endpoint to generate JWT.", user.PhoneNumber)
				: string.Format("Account Confirmed for Phone Number {0}. You should confirm your E-mail before using the /api/tokens endpoint to generate JWT."
				, user.PhoneNumber)
			: throw new InternalServerException(string.Format("An error occurred while confirming {0}", user.PhoneNumber));
	}
}
