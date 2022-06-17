
using NaturalVariety.Items.Critters;
using NaturalVariety.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{
    public class GoldSeagull : BaseWaterfowl
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/GoldSeagull";


        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPCID.Sets.NormalGoldCritterBestiaryPriority.Add(Type);
            NPCID.Sets.GoldCrittersCollection.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<GoldSeagullItem>();
            NPC.rarity = 3;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                // BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "The golden seagull (Larus midas) is a gull species endemic to Terraria's beaches. It is classified as endangered (EN) in the IUCN red list, due to illegal captures for their precious plumage.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnHelper.GoldCritterModifier(spawnInfo, SpawnHelper.SeagullChance(spawnInfo));
        }

        public override void AI()
        {
            base.AI();
            if (Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(SoundID.Seagull);
            }

            GoldenCritterParticleEffects();
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            GoldenCritterHitEffect(hitDirection, damage);
        }
    }
}

