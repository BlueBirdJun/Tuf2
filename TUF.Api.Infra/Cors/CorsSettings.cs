using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUF.Api.Infra.Cors;

public class CorsSettings
{
    public string? Angular { get; set; }
    public string? Blazor { get; set; }
    public string? React { get; set; }
}