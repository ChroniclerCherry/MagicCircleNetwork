using StardewModdingAPI;

namespace MagicCircleNetwork.Arboretum
{
    class Arboretum
    {
        IModHelper helper;
        IMonitor monitor;
        public Arboretum(IModHelper helper, IMonitor monitor)
        {
            this.helper = helper;
            this.monitor = monitor;
        }

    }
}
