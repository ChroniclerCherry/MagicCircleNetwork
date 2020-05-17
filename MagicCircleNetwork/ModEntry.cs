using StardewModdingAPI;

namespace MagicCircleNetwork
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            //Add initialization of each part of the code here. Pass in helper and monitor
            //that should be the extent of code in this class

            new Arboretum.Arboretum(helper, Monitor);
        }
    }
}
