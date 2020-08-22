using Prism.Commands;
using Prism.Mvvm;
using PrismBotV2.Modules.Info.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismBotV2.Modules.Info.ViewModels
{
    public class AccountViewModel : BindableBase
    {

        public Player Player { get; set; } = new Player();

        public AccountViewModel()
        {

        }
    }

}
