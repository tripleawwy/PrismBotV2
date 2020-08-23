using Prism.Commands;
using Prism.Mvvm;
using PrismBotV2.Modules.Info.Models;
using PrismBotV2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismBotV2.Modules.Info.ViewModels
{
    public class AccountViewModel : BindableBase
    {
        public AccountViewModel(IProcessMonitor processMonitor)
        {
            this.processMonitor = processMonitor;
        }

        public Player Player { get; set; } = new Player();

        public IDictionary<int, Process> kalClients;

        private DelegateCommand<object> _addPlayer;
        private readonly IProcessMonitor processMonitor;

        public DelegateCommand<object> AddPlayer =>
            _addPlayer ?? (_addPlayer = new DelegateCommand<object>(ExecuteAddPlayer));

        void ExecuteAddPlayer(object parameter)
        {
            kalClients = processMonitor.FindKalClients();
        }

    }

}
