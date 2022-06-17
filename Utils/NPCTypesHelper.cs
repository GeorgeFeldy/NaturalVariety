using NaturalVariety.Items.Critters;
using NaturalVariety.NPCs.Critters;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace NaturalVariety.Utils
{

    /// <summary>
    /// List of NPC and Item IDs by categories in this mod, for various uses
    /// </summary>
	public static class NPCTypesHelper
    {
        // general list for critter and critter items in this mod 
        public static readonly List<int> Critters;
        public static readonly List<int> CritterItems;

        // Lists of all critter NPC types in this mod 
        public static readonly List<int> Songbirds;
        public static readonly List<int> Ducks;
        public static readonly List<int> Flamingoes;
        public static readonly List<int> Frogs;
        public static readonly List<int> GoldenCritters;


        // Lists of all critter item types in this mod
        public static readonly List<int> SongbirdItems;
        public static readonly List<int> DuckItems;
        public static readonly List<int> FlamingoeItems;
        public static readonly List<int> FrogItems;
        public static readonly List<int> GoldenCritterItems;

        static NPCTypesHelper()
        {
            Songbirds = new()
            {
                ModContent.NPCType<Blackbird>(),
                ModContent.NPCType<EurasianJay>(),
                ModContent.NPCType<Goldfinch>(),
                ModContent.NPCType<BeeEater>(),
                ModContent.NPCType<Kingfisher>()
            };

            Ducks = new()
            {
                ModContent.NPCType<Pochard>(),
                ModContent.NPCType<FerruginousDuck>(),
                ModContent.NPCType<GoldDuck>(),
                ModContent.NPCType<GoldSeagull>()
            };

            Flamingoes = new()
            {
                ModContent.NPCType<FlamingoPink>(),
                ModContent.NPCType<FlamingoWhite>(),
                ModContent.NPCType<FlamingoGold>()
            };

            Frogs = new()
            {
                ModContent.NPCType<FrogBlue>(),
                ModContent.NPCType<FrogRed>(),
                ModContent.NPCType<FrogYellow>(),
            };

            GoldenCritters = new()
            {
                ModContent.NPCType<GoldDuck>(),
                ModContent.NPCType<GoldSeagull>(),
                ModContent.NPCType<FlamingoGold>()
            };

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
                ModContent.ItemType<FlamingoItem>(),
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

            Critters = new();
            Critters.AddRange(Songbirds);
            Critters.AddRange(Ducks);
            Critters.AddRange(Flamingoes);
            Critters.AddRange(Frogs);
            Critters.AddRange(GoldenCritters);

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