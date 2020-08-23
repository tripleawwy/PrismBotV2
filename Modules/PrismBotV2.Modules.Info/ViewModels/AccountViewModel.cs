using Prism.Commands;
using Prism.Mvvm;
using PrismBotV2.Modules.Info.Models;
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

        public Player Player { get; set; } = new Player();

        public ObservableCollection<Process> kalClients = new ObservableCollection<Process>();

        private DelegateCommand<object> _addPlayer;
        public DelegateCommand<object> AddPlayer =>
            _addPlayer ?? (_addPlayer = new DelegateCommand<object>(ExecuteAddPlayer));

        void ExecuteAddPlayer(object parameter)
        {
            kalClients = new ObservableCollection<Process>(ProcessMonitor.FindKalClients());
        }

        public AccountViewModel()
        {

        }
    }

}
