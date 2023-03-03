using Microsoft.AspNetCore.Components;
using MudBlazor;
using TUF.Client.Client.Areas.ChatingRoom.Models;
using TUF.Client.Client.Components.Board;
using TUF.Client.Shared.Dtos.Boards;

namespace TUF.Client.Client.Areas.ChatingRoom;

public partial  class Room
{
    #region inject
    [Parameter] public AvatarModel? modeldata { get; set; }
    #endregion

    #region parameter
    [Parameter]
    public string Kind { get; set; }
    #endregion

    #region property
    public async Task CreateRoom()
    {

    }

    #endregion
}
