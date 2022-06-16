using NaturalVariety.Items.Critters;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{
    public class FlamingoWhite : BaseWadingBird
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Flamingo");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<FlamingoWhiteItem>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f; // can only spawn via Flamingo spawner
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "The greater flamingo (Phoenicopterus roseus) is the most widespread and largest species of the flamingo family." +
                   " It is found in Africa, the Indian subcontinent, the Middle East, and in southern Europe." +
                   " Most of its plumage is white, but the primary and secondary flight feathers are black.")
            });
        }

        public override void AI()
        {
            base.AI();

        }
    }

}

