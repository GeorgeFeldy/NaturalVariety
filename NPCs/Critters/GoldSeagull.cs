
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
    public class GoldSeagull : Waterfowl 
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
            // TODO: adjust chances based on player luck 
            //spawnInfo.Player.RollLuck(NPC.goldCritterChance);
            
            float baseWaterChance = SpawnCondition.OverworldWaterSurfaceCritter.Chance * 0.00125f; // 1/800 water surface (for spawning in water)
            float baseLandChance = SpawnCondition.Overworld.Chance * 0.00125f; // 1/800 any surface enemy (for spawning on sand or shell piles)

            bool beachCondition = (spawnInfo.SpawnTileX < 250 || spawnInfo.SpawnTileX > Main.maxTilesX - 250);

            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            bool sandCondition = (tile.TileType == TileID.Sand) || (tile.TileType == TileID.ShellPile); 

            float chance = (beachCondition && sandCondition) ? (baseWaterChance + baseLandChance) : 0f;

            return chance;
        }

        public override void AI()
        {
            base.AI();
            if(Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(SoundID.Seagull);    
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

    public class GoldSeagullFly : WaterfowlFly
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/GoldSeagull";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Gold Seagull");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<GoldSeagullItem>();
            NPC.rarity = 3;
        }

        public override void AI()
        {
            base.AI();

            if (Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(SoundID.Seagull);
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

