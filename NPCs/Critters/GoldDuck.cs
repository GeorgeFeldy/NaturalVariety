
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;

using NaturalVariety.Items.Critters;


namespace NaturalVariety.NPCs.Critters
{
    public class GoldDuck : Waterfowl 
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

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "The golden duck (Anas aurus) is a near-threatened species of duck with distinctive golden plumage. It is a common victim of illegal captures due to it's high value amongst collectors.")
            });
        }


        public override void AI()
        {
            base.AI();
            if(Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(SoundID.Duck);    
            }

            if (Main.rand.NextBool(20))
            {
                int goldDust = Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.Next(232, 234), 0, 0, 20); // not synced 
                Main.dust[goldDust].velocity *= 0;
                Main.dust[goldDust].noGravity = true;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            // full override (no call to base) 

            if (NPC.life > 0)
            {
                for (int idx = 0; idx < 10; idx++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.Next(232, 234), hitDirection, -1f);
                }
            }
            else
            {
                for (int idx = 0; idx < 20; idx++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.Next(232, 234), 2 * hitDirection, -2f);
                }
            }
        }
    }

    public class GoldDuckFly : WaterfowlFly
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/GoldDuck";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Gold Duck");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<GoldDuckItem>();
            NPC.rarity = 3;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = SpawnCondition.OverworldWaterSurfaceCritter.Chance * 0.00250f;
            return chance;
        }

        public override void AI()
        {
            base.AI();

            if (Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(SoundID.Duck);
            }

            if (Main.rand.NextBool(20))
            {
                int goldDust = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GoldCritter, 0, 0, 20);
                Main.dust[goldDust].velocity *= 0;
                Main.dust[goldDust].noGravity = true;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            // full override (no call to base) 

            if (NPC.life > 0)
            {
                for (int idx = 0; idx < 10; idx++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.Next(232, 234), hitDirection, -1f);
                }
            }
            else
            {
                for (int idx = 0; idx < 20; idx++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.Next(232, 234), 2 * hitDirection, -2f);
                }
            }
        }

    }

}

