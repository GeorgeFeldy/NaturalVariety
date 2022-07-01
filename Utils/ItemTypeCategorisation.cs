using NaturalVariety.Items.Critters;
using NaturalVariety.NPCs.Critters;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace NaturalVariety.Utils
{

    /// <summary>
    /// List of critter Item IDs by categories in this mod, for various uses
    /// </summary>
	public static class ItemTypeCategorisation
    {
        // general list for critter and critter items in this mod 
        public static readonly List<int> CritterItems;

        // Lists of all critter item types in this mod
        public static readonly List<int> SongbirdItems;
        public static readonly List<int> DuckItems;
        public static readonly List<int> FlamingoeItems;
        public static readonly List<int> FrogItems;
        public static readonly List<int> GoldenCritterItems;

        static ItemTypeCategorisation()
        {
           
            SongbirdItems = new()
            {
                ModContent.ItemType<BlackbirdItem>(),
                ModContent.ItemType<EurasianJayItem>(),
                ModContent.ItemType<GoldfinchItem>(),
                ModContent.ItemType<BeeEaterItem>(),
                ModContent.ItemType<KingfisherItem>()
            };

            DuckItems = new()
            {
                ModContent.ItemType<PochardItem>(),
                ModContent.ItemType<FerruginousDuckItem>(),
                ModContent.ItemType<GoldDuckItem>(),
                ModContent.ItemType<GoldSeagullItem>()
            };

            FlamingoeItems = new()
            {
                ModContent.ItemType<FlamingoPinkItem>(),
                ModContent.ItemType<FlamingoWhiteItem>(),
                ModContent.ItemType<FlamingoGoldItem>()
            };

            FrogItems = new()
            {
                ModContent.ItemType<FrogBlueItem>(),
                ModContent.ItemType<FrogRedItem>(),
                ModContent.ItemType<FrogYellowItem>(),
            };

            GoldenCritterItems = new()
            {
                ModContent.ItemType<GoldDuckItem>(),
                ModContent.ItemType<GoldSeagullItem>(),
                ModContent.ItemType<FlamingoGoldItem>()
            };


            CritterItems = new();
            CritterItems.AddRange(SongbirdItems);
            CritterItems.AddRange(DuckItems);
            CritterItems.AddRange(FlamingoeItems);
            CritterItems.AddRange(FrogItems);
            CritterItems.AddRange(GoldenCritterItems);
        }

        /// <summary>
        /// Utility function that returns all elements in list1 not found in list2
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static List<int> NotIntersect(List<int> list1, List<int> list2)
        {
            List<int> result = new();
            foreach (int i in list1)
            {
                if (!list2.Contains(i))
                {
                    result.Add(i);
                }
            }
            return result;
        }

        /// <summary>
        /// Get a copy of list with "item" type removed
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<int> NotIntersect(List<int> list, int item)
        {
            List<int> result = list;
            result.Remove(item);
            return result;
        }
    }
}