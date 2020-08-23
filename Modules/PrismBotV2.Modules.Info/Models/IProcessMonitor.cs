using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Documents;

namespace PrismBotV2.Modules.Info.Models
{
    public interface IProcessMonitor
    {
        IList<Process> FindKalClients();
    }
}