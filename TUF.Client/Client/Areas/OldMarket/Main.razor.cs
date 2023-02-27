using Knus.Common.Helpers;
using Knus.Common.Services;
using MudBlazor;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Timers;
using TUF.Client.Client.Areas.OldMarket.Views;
using TUF.Client.Client.Components.Board;
using TUF.Client.Client.Components.Common;
using TUF.Client.Shared.Authorization;
using TUF.Client.Shared.Bodys.Boards;
using TUF.Client.Shared.Bodys.OldMarket;
using TUF.Client.Shared.Dtos.Boards;
using TUF.Client.Shared.Dtos.OldMarket;
using static MudBlazor.Colors;

namespace TUF.Client.Client.Areas.OldMarket;

public partial class Main
{
    private List<string> states =new List<string>
    {
        "내셔널지오그래픽", "다이나핏", "스노우피크", "코닥" 
    };

    private TimeSpan ts = new TimeSpan(0, 0, 3);
    private string value1;
    private bool resetValueOnEmptyText=true;
    private bool coerceText=true;
    private bool coerceValue = true;
    private bool RefreshOn = false;
    public int spacing { get; set; } = 2;

    public int RefreshTime { get; set; } = 20;
    public int RemainTime { get; set; } = 1;

    private int nowScope { get; set; } = 0;

    BungaeDto.Request param = new();
    private CustomValidation? _customValidation;
    IEnumerable<BungaeModel> lstproduct { get; set; }// = new List<BungaeModel>();
    [Required(ErrorMessage = "검색어 필수")]
    string SearchValue { get; set; }
    protected override async Task OnInitializedAsync()
    {
        _timer = new();
        _timer.Interval = 1000;
        _timer.Elapsed += async (object? sender, ElapsedEventArgs e) =>
        {
            if (RemainTime < RefreshTime)
                RemainTime++;
            else
            {
                _timer.Enabled = false;
                param.Keyword = states[nowScope];
                if (!param.Keyword.IsNullOrEmpty())
                {
                    await SearchButton();

                }
                RemainTime = 0;
                nowScope++;
                if(nowScope == states.Count)
                {
                    nowScope = 0;
                }
                _timer.Enabled = true;               
            }

            await InvokeAsync(StateHasChanged);
        };
        //_timer.Enabled = true;
        param.Keyword = states[0];
        await SearchButton();
    }
    DateTime lasttime = DateTime.Now;
    protected async Task SearchButton()
    { 
        lstproduct = null;
        if(! states.Where(p=>p == param.Keyword).Any())
        {
            states.Add(param.Keyword);
        }
        ApiProvider<BungaeDto> api = new ApiProvider<BungaeDto>();
        BungaeDto.Request param1 = new BungaeDto.Request();
        param1 = param;
        api.SendValue = JsonConvert.SerializeObject(param1);
        var rt = await apihelper.ExecuteCall(api, BungaeDto.GetMeta);
        //lstproduct.Clear(); 
        if (rt.Success)
        {
            lstproduct = rt.OutValue.OutPutValue.Products;
        }
        var ts1 = DateTime.Now - lasttime;
        
        Snackbar.Add($"{ts1.Seconds.ToString()}초 전에 {lstproduct.Count()} 가져옴", Severity.Info);
        lasttime = DateTime.Now;
        StateHasChanged();
        await Task.Delay(1000);
    }

    private async Task<IEnumerable<string>> Search1(string value)
    {
        // In real life use an asynchronous function for fetching data from an api.
        await Task.Delay(5);

        // if text is null or empty, show complete list
        if (string.IsNullOrEmpty(value))
            return states;
        return states.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private async Task QuickButton(string arg)
    {
        Snackbar.Add(arg, MudBlazor.Severity.Info);
        param.Keyword = arg;
        await SearchButton();
    }

    private async Task DetailProduct(BungaeModel arg)
    {
        Snackbar.Add(arg.Title, MudBlazor.Severity.Info);
        DialogOptions disableBackdropClick = new DialogOptions() { DisableBackdropClick = false, MaxWidth = MaxWidth.Large, FullWidth = true };
        var parameters = new DialogParameters();
        parameters.Add("modeldata", arg);
        DialogService.Show<ProductDetail>("상품", parameters, disableBackdropClick);
    }

    private System.Timers.Timer _timer;
     
    public void OnToggledChanged(bool toggled)
    {
        // Because variable is not two-way bound, we need to update it ourself
        RefreshOn = toggled;

        if(RefreshOn)
        {
            _timer.Enabled = true;
        }
        else
            _timer.Enabled = false;
    }

}
