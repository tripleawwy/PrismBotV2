using System.Collections.Generic;
using System.Diagnostics;

namespace PrismBotV2.Services.Interfaces
{
    public interface IProcessMonitor
    {
        IDictionary<int, Process> FindKalClients();
    }
}