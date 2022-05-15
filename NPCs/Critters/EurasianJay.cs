using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

using NaturalVariety.Items.Critters;
// using NaturalVariety.Items.Placeable.Banners;

namespace NaturalVariety.NPCs.Critters
{
    public class EurasianJay : Songbird 
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<EurasianJayItem>();
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
                    "Eurasian Jay (Garrulus glandarius) is a species of passerine bird in the family Corvidae with boldly patterned " +
                    "plumage, typically having blue feathers in the wings or tail.")
            });
        }

      
    }
}