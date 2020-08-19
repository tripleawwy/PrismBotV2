using PrismBotV2.Modules.Utilities.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismBotV2.Core;

namespace PrismBotV2.Modules.Utilities
{
    public class UtilitiesModule : IModule
    {
        private readonly IRegionManager regionManager;

        public UtilitiesModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(UtilityBoxView));
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}