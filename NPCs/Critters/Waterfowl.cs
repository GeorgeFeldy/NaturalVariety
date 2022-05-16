using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace NaturalVariety.NPCs.Critters
{

    /// <summary>
    /// Generic class for all swimming duck-like critters in this mod 
    /// </summary>
    public abstract class Waterfowl : Critter 
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Duck];           

            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)                   
            {
                Velocity = 1f,
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);                    
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldWaterSurfaceCritter.Chance; // Spawn with regular bird chance. 
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 22;                    
            NPC.height = 26;                   
            NPC.aiStyle = NPCAIStyleID.Passive;                                                              
            AnimationType = NPCID.Duck;                                        
                    
            Banner = Item.NPCtoBanner(NPCID.Duck);
            BannerItem = Item.BannerToItem(Banner);
        }
    }

    public abstract class WaterfowlFly : Critter
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Duck2];          

            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)                   
            {
                Position = new Vector2(3f, 0f),
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 28;
            NPC.height = 22;
            NPC.aiStyle = NPCAIStyleID.Duck;
            AnimationType = NPCID.Duck2;                  

            Banner = Item.NPCtoBanner(NPCID.Duck);
            BannerItem = Item.BannerToItem(Banner);
        }

    }

}