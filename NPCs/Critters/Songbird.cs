using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{

    /// <summary>
    /// Generic class for all "Bird AI" type critters in this mod 
    /// </summary>
    public abstract class Songbird : ModNPC 
    {

        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault(" ");
            Main.npcCatchable[Type] = true;
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Bird];           // frame count of bird critter, returns 4 
            NPCID.Sets.CountsAsCritter[Type] = true;                             // all songbirds are critters
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;                       // hardmode scaling not needed for critters
            NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true; // can be killed by enemies :(

            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)                   // Bestiary position
            {
                Position = new Vector2(1f, -14f),
                Velocity = 0.05f,
                PortraitPositionYOverride = -30f

            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);                  
            
        }

        public override void SetDefaults()
        {
            NPC.width = 14;                    
            NPC.height = 14;                   
            NPC.defense = 0;                
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0;
            NPC.knockBackResist = 0.8f;
            NPC.aiStyle = 24;           
            NPC.alpha = 0;                                                      // opaque !!! 
            AnimationType = NPCID.Bird;                                         // Use vanilla bird's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
                    
            Banner = Item.NPCtoBanner(NPCID.Bird);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;

        // public override float SpawnChance(NPCSpawnInfo spawnInfo) {
		// 	return SpawnCondition.OverworldDayBirdCritter.Chance; // Spawn with regular bird chance.
		// }


        // public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        // {
        //     bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        //     {
        //         BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
        //         BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
        // 
        //         new FlavorTextBestiaryInfoElement(
        //             "Hee hee")
        //     });
        // }

      
    }
}