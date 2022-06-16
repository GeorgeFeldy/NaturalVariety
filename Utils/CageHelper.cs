using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace NaturalVariety.Utils
{
    /// <summary>
    /// Helper for storing all item IDs that are "cage" type 
    /// </summary>
    public static class CageHelper
    {
        /// <summary>
        /// list of item IDs that are cage-like, stored by ID ascending
        /// </summary>
        public static readonly List<int> cageList;

        /// <summary>
        /// dictionary of <cageID, List of items in its recipe>
        /// </summary>
        public static readonly Dictionary<int, List<Item>> recipeItemDict;

        static CageHelper()
        {
            cageList = new() // list of "cage" type items, sorted by ID 
            {
                ItemID.FishBowl,
                ItemID.StarinaBottle,
                ItemID.FireflyinaBottle,
                ItemID.LightningBuginaBottle,
                ItemID.BunnyCage,
                ItemID.SquirrelCage,
                ItemID.MallardDuckCage,
                ItemID.DuckCage,
                ItemID.BirdCage,
                ItemID.BlueJayCage,
                ItemID.CardinalCage,
                ItemID.SnailCage,
                ItemID.GlowingSnailCage,
                ItemID.ScorpionCage,
                ItemID.BlackScorpionCage,
                ItemID.FrogCage,
                ItemID.MouseCage,
                ItemID.PenguinCage,
                ItemID.WormCage,
                ItemID.GrasshopperCage,
                ItemID.GoldBirdCage,
                ItemID.GoldBunnyCage,
                ItemID.GoldButterflyCage,
                ItemID.GoldFrogCage,
                ItemID.GoldGrasshopperCage,
                ItemID.GoldMouseCage,
                ItemID.GoldWormCage,
                ItemID.CageEnchantedNightcrawler,
                ItemID.CageBuggy,
                ItemID.CageGrubby,
                ItemID.CageSluggy,
                ItemID.SquirrelOrangeCage,
                ItemID.SquirrelGoldCage,
                ItemID.GoldGoldfishBowl,
                ItemID.MaggotCage,
                ItemID.RatCage,
                ItemID.LadybugCage,
                ItemID.OwlCage,
                ItemID.PupfishBowl,
                ItemID.GoldLadybugCage,
                ItemID.TurtleCage,
                ItemID.TurtleJungleCage,
                ItemID.GrebeCage,
                ItemID.SeagullCage,
                ItemID.WaterStriderCage,
                ItemID.GoldWaterStriderCage,
                ItemID.SeahorseCage,
                ItemID.GoldSeahorseCage,
                ItemID.SoulBottleLight,
                ItemID.SoulBottleNight,
                ItemID.SoulBottleFlight,
                ItemID.SoulBottleSight,
                ItemID.SoulBottleMight,
                ItemID.SoulBottleFright,
                ItemID.LavaflyinaBottle,
                ItemID.MagmaSnailCage,
                ItemID.AmethystBunnyCage,
                ItemID.TopazBunnyCage,
                ItemID.SapphireBunnyCage,
                ItemID.EmeraldBunnyCage,
                ItemID.RubyBunnyCage,
                ItemID.DiamondBunnyCage,
                ItemID.AmberBunnyCage,
                ItemID.AmethystSquirrelCage,
                ItemID.TopazSquirrelCage,
                ItemID.SapphireSquirrelCage,
                ItemID.EmeraldSquirrelCage,
                ItemID.RubySquirrelCage,
                ItemID.DiamondSquirrelCage,
                ItemID.AmberSquirrelCage,
                ItemID.TruffleWormCage
            };

            recipeItemDict = new();
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe itemRecipe = Main.recipe[i];
                Item recipeResult = itemRecipe.createItem;

                if (IsCageItem(recipeResult))
                {
                    if (!recipeItemDict.ContainsKey(recipeResult.type))  // will add only the first recipe it finds (TODO: check if it's always vanilla)
                    {
                        recipeItemDict.Add(recipeResult.type, itemRecipe.requiredItem);
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if item is cage type found in the list 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool IsCageItem(Item item)
        {
            return (cageList.BinarySearch(item.type) >= 0);
        }
    }
}
