using MagicCircleNetwork.Utility;
using StardewModdingAPI;

namespace MagicCircleNetwork
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            //Initialization of APIs
            APIs.Initialize(Monitor, Helper);
            APIs.RegisterJsonAssets();

            //Add initialization of each part of the code here. Pass in helper and monitor
            //that should be the extent of code in this class
            new Arboretum.ArboretumCrops(helper, Monitor);
        }
    }
}
