using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAWService.Application.Common.Options;
public class ConnectionStringsOptions
{
    public const string ConnectionStrings = "ConnectionStrings";

    public string SqlDatabase { get; set; } = string.Empty;
}