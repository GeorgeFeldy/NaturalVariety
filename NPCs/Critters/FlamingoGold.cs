using NaturalVariety.Items.Critters;
using NaturalVariety.Sounds;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{
    public class FlamingoGold : BaseWadingBird
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Gold Flamingo");
            NPCID.Sets.NormalGoldCritterBestiaryPriority.Add(Type);
            NPCID.Sets.GoldCrittersCollection.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<FlamingoGoldItem>();
            NPC.rarity = 3;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => 0f; // can only spawn via Flamingo spawner

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "The golden flamingo (Phoenicopterus midas) is a rare species of flamingo. It is found amongst other flamingoes in the genus Phoenicopterus, " +
                   "but it's critically endangered due to poaching for it's highly valued plumage and significant consumption as a delicacy in some countries.")
            });
        }

        public override void AI()
        {
            base.AI();

            GoldenCritterParticleEffects();
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            GoldenCritterHitEffect(hitDirection, damage);
        }

    }

}

