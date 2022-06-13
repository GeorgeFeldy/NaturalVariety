using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

using NaturalVariety.Items.Critters;
using NaturalVariety.Utils;

namespace NaturalVariety.NPCs.Critters
{ 
    public class Kingfisher : Songbird 
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<KingfisherItem>();
        }
     
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                    "The common kingfisher (Alcedo atthis), also known as the Eurasian kingfisher and river kingfisher, is a small kingfisher found across Eurasia and North Africa. It has the typical short-tailed, large-headed kingfisher profile; it has blue upperparts, orange underparts and a long bill. It feeds mainly on fish, caught by diving, and has special visual adaptations to enable it to see prey under water."
                    )
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return SpawnHelper.JungleBirdChance(spawnInfo);

        }

    }
}