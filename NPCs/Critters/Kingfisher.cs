using NaturalVariety.Items.Critters;
using NaturalVariety.Utils;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{
    public class Kingfisher : BaseSongbird
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
                    "The common kingfisher (Alcedo atthis), also known as the river kingfisher, is a small kingfisher species. It has the typical short-tailed, large-headed kingfisher profile; it has blue upperparts, orange underparts and a long bill. It feeds mainly on fish, caught by diving, and has special visual adaptations to enable it to see prey under water."
                    )
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnHelper.JungleBirdChance(spawnInfo);

    }
}