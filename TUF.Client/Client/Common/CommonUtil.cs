﻿using Microsoft.JSInterop;

namespace TUF.Client.Client.Common
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
