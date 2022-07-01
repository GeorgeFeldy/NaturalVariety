using NaturalVariety.Items.Critters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{

    /// <summary>
    /// Generic class for all critters in this mod 
    /// </summary>
    public abstract class BaseCritter : ModNPC
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


        public void GoldenCritterHitEffect(int hitDirection, double damage)
        {
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

        public void GoldenCritterParticleEffects()
        {
            if (Main.rand.NextBool(20))
            {
                int goldDust = Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.Next(232, 234), 0, 0, 20);
                Main.dust[goldDust].velocity *= 0;
                Main.dust[goldDust].noGravity = true;
            }
        }
    }
}