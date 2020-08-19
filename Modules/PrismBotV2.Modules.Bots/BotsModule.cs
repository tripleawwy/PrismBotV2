using PrismBotV2.Modules.Bots.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismBotV2.Core;

namespace PrismBotV2.Modules.Bots
{
    public class BotsModule : IModule
    {
        private readonly IRegionManager regionManager;

        public BotsModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(BotsItem));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}