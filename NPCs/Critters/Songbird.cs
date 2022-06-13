using Microsoft.Xna.Framework;
using NaturalVariety.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace NaturalVariety.NPCs.Critters
{
    /// <summary>
    /// Generic class for all "Bird AI" type critters in this mod 
    /// </summary>
    public abstract class Songbird : Critter 
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Bird];           

            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)                   
            {
                Position = new Vector2(1f, -14f),
                Velocity = 0.05f,
                PortraitPositionYOverride = -30f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);                  
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnHelper.ForestBirdChance(spawnInfo);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 14;                    
            NPC.height = 14;                   
            NPC.aiStyle = NPCAIStyleID.Bird;                                                   
            AnimationType = NPCID.Bird; // Use vanilla bird's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
                    
            Banner = Item.NPCtoBanner(NPCID.Bird);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life > 0)
            {
                for (int idx = 0; (double)idx < damage / (double)NPC.lifeMax * 20.0; idx++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hitDirection, -1f);
                }

                return;
            }

            if (Main.netMode == NetmodeID.Server)
            {
                return; // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers
            }

            if(NPC.life <= 0)
            {
                int songbirdGore;

                string className = this.GetType().Name;

                try
                {
                    songbirdGore = Mod.Find<ModGore>(className + "_Gore").Type;
                }
                catch
                {
                    songbirdGore = 100;
                }

                var entitySource = NPC.GetSource_Death();

                Gore.NewGore(entitySource, NPC.position, NPC.velocity, songbirdGore);
            }

        }

    }
}