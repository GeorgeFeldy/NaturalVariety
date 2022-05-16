using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{

    /// <summary>
    /// Generic class for all critters in this mod 
    /// </summary>
    public abstract class Critter : ModNPC 
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault(" ");
            Main.npcCatchable[Type] = true;
            NPCID.Sets.CountsAsCritter[Type] = true;                             
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;                       
            NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;            
        }

        public override void SetDefaults()
        {             
            NPC.defense = 0;                
            NPC.lifeMax = 5;
            NPC.value = 0;
            NPC.knockBackResist = 0.8f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.alpha = 0;                                                                                              
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;
    }
}