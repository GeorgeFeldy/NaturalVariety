using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

using NaturalVariety.Items.Critters;
// using NaturalVariety.Items.Placeable.Banners;

namespace NaturalVariety.NPCs.Critters
{
    public class Blackbird: Songbird 
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<BlackbirdItem>();
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
                    "Blackbird (Turdus merula) is an Old World thrush in the family Turdidae. The male is all black except for " +
                    "a yellow eye-ring and bill and has a rich, melodious song; the adult female and juvenile have mainly dark brown plumage. ")
            });
        }

      
    }
}