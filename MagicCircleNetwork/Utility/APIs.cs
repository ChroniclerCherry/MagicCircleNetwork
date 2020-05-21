using StardewModdingAPI;
using System.Collections.Generic;

namespace MagicCircleNetwork.Utility
{
    class APIs
    {
        public static IJsonAssetsApi JsonAssets;
        public static IMonitor Monitor;
        public static IModHelper Helper;

        public static void Initialize(IMonitor m,IModHelper h)
        {
            Monitor = m;
            Helper = h;
        }

        public static void RegisterJsonAssets()
        {
            JsonAssets = Helper.ModRegistry.GetApi<IJsonAssetsApi>("spacechase0.JsonAssets");

        }

    }
    public interface IJsonAssetsApi
    {
        List<string> GetAllCropsFromContentPack(string cp);
        int GetCropId(string name);
    }
}
