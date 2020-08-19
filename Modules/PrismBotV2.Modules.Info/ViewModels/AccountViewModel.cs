using Prism.Commands;
using Prism.Mvvm;
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
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ObservableCollection<Person> PersonList { get; set; } = new ObservableCollection<Person>();

        public AccountViewModel()
        {
            Message = "View A from your Prism Module";
            PersonList.Add(new Person { Age = 1, Name = "björn" });
            PersonList.Add(new Person { Age = 14, Name = "robi" });
        }
    }

    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }
}
