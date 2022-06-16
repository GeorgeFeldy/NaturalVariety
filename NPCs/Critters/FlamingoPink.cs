using NaturalVariety.Items.Critters;
using NaturalVariety.Sounds;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{
    public class FlamingoPink : BaseWadingBird
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Flamingo");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<FlamingoItem>();
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
                   "The greater flamingo (Phoenicopterus roseus) is the most widespread and largest species of the flamingo family. " +
                   "It is found in Africa, the Indian subcontinent, the Middle East, and in southern Europe. " +
                   "The bright pink color of some flamingoes comes from beta-carotene, a red-orange pigment that’s found in high amounts within the algae, " +
                   "brine fly larvae, and brine shrimp that flamingos eat in their wetland environment.")
            });
        }

        public override void AI()
        {
            base.AI();
            if (Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(CustomSounds.FlamingoCall, NPC.position);
            }
        }
    }

}

