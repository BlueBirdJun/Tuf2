using TUF.Client.Client.Components.Common;
using TUF.Client.Shared.Dtos.Member;

namespace TUF.Client.Client.Areas.Identity;

public partial class ForgotPassword
{
    private readonly ForgotPasswordDto _forgotPasswordRequest = new();
    private CustomValidation? _customValidation;
    private bool BusySubmitting { get; set; }
    private async Task SubmitAsync()
    {

    }

}

