
using NaturalVariety.Items.Critters;
using NaturalVariety.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{
    public class GoldDuck : BaseWaterfowl
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/GoldDuck";


        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPCID.Sets.NormalGoldCritterBestiaryPriority.Add(Type);
            NPCID.Sets.GoldCrittersCollection.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<GoldDuckItem>();
            NPC.rarity = 3;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnHelper.GoldCritterModifier(spawnInfo, SpawnHelper.DuckChance(spawnInfo));
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "The golden duck (Anas midas) is a near-threatened species of duck with distinctive golden plumage. It is a common victim of illegal captures due to it's high value amongst collectors.")
            });
        }

        public override void AI()
        {
            base.AI();
            if (Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(SoundID.Duck);
            }

            GoldenCritterParticleEffects();
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            GoldenCritterHitEffect(hitDirection, damage);
        }
    }

}

