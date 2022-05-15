using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

using NaturalVariety.Items.Critters;
// using NaturalVariety.Items.Placeable.Banners;

namespace NaturalVariety.NPCs.Critters
{
    public class Goldfinch : Songbird 
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<GoldfinchItem>();
            // Banner = Item.NPCtoBanner(NPCID.Bird);
            // BannerItem = Item.BannerToItem(Banner);
        }


        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			return SpawnCondition.OverworldDayBirdCritter.Chance; // Spawn with regular bird chance. 
		}


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                    "The European goldfinch or simply the Goldfinch (Carduelis carduelis) is a small passerine bird in the finch family " +
                    "Fringillidae that is native to Europe, North Africa and western and central Asia. The breeding male has a red face " +
                    "with black markings around the eyes, and a black-and-white head. The back and flanks are buff or chestnut brown. " +
                    "The black wings have a broad yellow bar. The tail is black and the rump is white. Males and females are very similar, " +
                    "but females have a slightly smaller red area on the face.")
            });
        }

      
    }
}