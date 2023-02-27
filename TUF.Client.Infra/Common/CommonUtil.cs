using Microsoft.JSInterop;

namespace TUF.Client.Infra.Common
{
    public class CommonUtil
    {
        private IJSRuntime _jsRuntime;
        public CommonUtil(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        
    }
}
