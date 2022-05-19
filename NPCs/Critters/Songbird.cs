using Microsoft.Xna.Framework;
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
            return SpawnCondition.OverworldDayBirdCritter.Chance; // Spawn with regular bird chance. 
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
      
    }
}