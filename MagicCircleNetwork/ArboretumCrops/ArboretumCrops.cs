using MagicCircleNetwork.Utility;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace MagicCircleNetwork.Arboretum
{
    class ArboretumCrops
    {
        IModHelper helper;
        IMonitor monitor;

        private static readonly string ArboretumBuildingInterior = "MCN.Arboretum";
        private static readonly string ArboretumCropsPack = "ParadigmNomad.FantasyCrops";
        private List<int> Crops;

        public ArboretumCrops(IModHelper helper, IMonitor monitor)
        {
            this.helper = helper;
            this.monitor = monitor;

            helper.Events.GameLoop.DayEnding += GameLoop_DayEnding;
            helper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;
        }

        private void GameLoop_SaveLoaded(object sender, StardewModdingAPI.Events.SaveLoadedEventArgs e)
        {
            foreach (string cropName in APIs.JsonAssets.GetAllCropsFromContentPack(ArboretumCropsPack)){
                Crops.Add(APIs.JsonAssets.GetCropId(cropName));
            }
        }

        private void GameLoop_DayEnding(object sender, StardewModdingAPI.Events.DayEndingEventArgs e)
        {
            foreach (GameLocation location in GetAllNonArboretumLocations())
            {
                KillCrops(location);
            }
        }

        /// <summary>
        /// Thanks to esca for figuring out pretty much all of how to do this. I shamelessly copied most of the logic from
        /// https://github.com/Esca-MMC/FarmTypeManager/blob/master/FarmTypeManager/Utility/Locations/GetAllLocationsFromName.cs
        /// </summary>
        /// <returns></returns>
        private IEnumerable<GameLocation> GetAllNonArboretumLocations()
        {
            foreach (GameLocation loc in Game1.locations)
            {
                yield return loc;
            }

            foreach (BuildableGameLocation buildable in Game1.locations.OfType<BuildableGameLocation>()) //for each buildable location in the game
            {
                foreach (Building building in buildable.buildings.Where(building => building.indoors.Value != null)) //for each building with an interior location ("indoors")
                {
                    yield return building.indoors.Value;
                }
            }

            if (Type.GetType("TMXLoader.TMXLoaderMod, TMXLoader") is Type tmx) //if TMXLoader can be accessed
            {
                if (tmx.GetField("buildablesBuild", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) is IList tmxSaveBuildables) //if tmx's SaveBuildables list can be accessed
                {
                    foreach (object sb in tmxSaveBuildables) //for each saved buildable in TMXLoader
                    {
                        if (sb.GetType() is Type sbType && sbType.GetProperty("UniqueId").GetValue(sb) is string UniqueId && sbType.GetProperty("Id").GetValue(sb) is string Id) //if this buildable's UniqueID and ID can be accessed
                        {
                            if (Id != ArboretumBuildingInterior)
                            {
                                string mapName = "BuildableIndoors-" + UniqueId; //construct the GameLocation.Name used for this buildable's interior location
                                yield return Game1.getLocationFromName(mapName);
                            }

                        }
                    }
                }
            }
        }

        private void KillCrops(GameLocation location)
        {
            this.monitor.Log($"Killing crops in {location.NameOrUniqueName}", LogLevel.Warn);

            foreach (TerrainFeature feature in location.terrainFeatures.Values)
            {
                if (feature is HoeDirt dirt)
                {

                }
            }
            ;
        }
    }

}
