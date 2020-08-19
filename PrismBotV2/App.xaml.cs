﻿using PrismBotV2.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using PrismBotV2.Modules.Info;
using PrismBotV2.Modules.Bots;
using PrismBotV2.Modules.Utilities;

namespace PrismBotV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<InfoModule>();
            moduleCatalog.AddModule<BotsModule>();
            moduleCatalog.AddModule<UtilitiesModule>();
        }

    }
}