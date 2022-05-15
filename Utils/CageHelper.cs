
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.Utils
{
    /// <summary>
    /// Helper for storing all item IDs that are "cage" type 
    /// </summary>
    public static class CageHelper
    {

        private static readonly List<int> cageList;  // only vanilla, ok to use int ID :-? 

        public static readonly Dictionary<int,List<Item>> recipeItemDict;  // dict item recipes 

        public static bool ItemIsCage(Item item)
        {
            bool isCage = false;    

            foreach (int cageItem in cageList)
            {
                if(item.type == cageItem)
                {
                    isCage = true;
                }
            }
            return isCage;    
        }

        static CageHelper()
        {
            cageList = new()
            {
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
                ItemID.GrasshopperCage,
                ItemID.GoldMouseCage,
                ItemID.GoldWormCage,
                ItemID.CageEnchantedNightcrawler,
                ItemID.CageBuggy,
                ItemID.CageGrubby,
                ItemID.CageSluggy,
                ItemID.SquirrelOrangeCage,
                ItemID.SquirrelGoldCage,
                ItemID.MaggotCage,
                ItemID.RatCage,
                ItemID.LadybugCage,
                ItemID.OwlCage,
                ItemID.GoldLadybugCage,
                ItemID.TurtleCage,
                ItemID.TurtleJungleCage,
                ItemID.GrebeCage,
                ItemID.SeagullCage,
                ItemID.WaterStriderCage,
                ItemID.GoldWaterStriderCage,
                ItemID.GrebeCage,
                ItemID.SeahorseCage,
                ItemID.GoldSeahorseCage,
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
                ItemID.TruffleWormCage,
                ItemID.FishBowl,
                ItemID.PupfishBowl,
                ItemID.GoldGoldfishBowl,
                ItemID.StarinaBottle,
                ItemID.SoulBottleLight,
                ItemID.SoulBottleNight,
                ItemID.SoulBottleFlight,
                ItemID.SoulBottleSight,
                ItemID.SoulBottleMight,
                ItemID.SoulBottleFright,
                ItemID.FireflyinaBottle,
                ItemID.LightningBuginaBottle,
                ItemID.LavaflyinaBottle
            };

            // bad code incoming? 
            recipeItemDict = new();
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe itemRecipe = Main.recipe[i];
                Item recipeResult = itemRecipe.createItem;

                if (ItemIsCage(recipeResult))
                {
                    if (!recipeItemDict.ContainsKey(recipeResult.type))  // will add only the first recipe it finds (TODO: check if it's always vanilla)
                    {
                        recipeItemDict.Add(recipeResult.type, itemRecipe.requiredItem);
                    }
                }
            }
        }
    }
}
