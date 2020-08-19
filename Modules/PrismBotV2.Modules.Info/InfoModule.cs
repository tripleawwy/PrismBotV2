using PrismBotV2.Modules.Info.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismBotV2.Core;

namespace PrismBotV2.Modules.Info
{
    public class InfoModule : IModule
    {
        private readonly IRegionManager regionManager;

        public InfoModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(AccountView));
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}