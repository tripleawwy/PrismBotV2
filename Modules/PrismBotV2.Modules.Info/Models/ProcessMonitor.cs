using PrismBotV2.Core.MemoryHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using static PrismBotV2.Core.DLLImports.Kernel32DLL;
using static PrismBotV2.Core.DLLImports.Kernel32DLL.ProcessAccessFlags;

namespace PrismBotV2.Modules.Info.Models
{
    public class ProcessMonitor : IProcessMonitor
    {
        private static readonly Dictionary<int, Process> kalClients = new Dictionary<int, Process>();
        private static readonly string[] clientNames = { "Engine", "engine" };

        public ProcessMonitor()
        {
            MonitorProcesses();
        }

        public Task MonitorProcesses()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    SearchKalClients();
                    Task.Delay(1000).Wait();
                }
            });
        }

        private static void SearchKalClients()
        {
            foreach (string name in clientNames)
            {
                foreach (Process process in Process.GetProcessesByName(name))
                {
                    if (!kalClients.ContainsKey(process.Id))
                    {
                        PrepareClient(process);
                        kalClients.Add(process.Id, process);
                    }
                }
            }
        }

        public IList<Process> FindKalClients()
        {
            return kalClients.Values.ToList();
        }

        private static void PrepareClient(Process client)
        {
            client.EnableRaisingEvents = true;
            client.Exited += new EventHandler(OnExit);
            OpenProcess(QueryInformation | VirtualMemoryRead | VirtualMemoryWrite | VirtualMemoryOperation, false, client.Id);
        }

        private static void OnExit(object sender, EventArgs e)
        {
            if (sender is Process process && kalClients.ContainsKey(process.Id))
            {
                kalClients.Remove(process.Id);
            }
        }

    }
}
