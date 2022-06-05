using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NaturalVariety.NPCs.Critters;


namespace NaturalVariety.NPCs.GlobalNPCs
{
    public class VanillaDuckAI : GlobalNPC
    {

        private enum ActionState
        {
            Walk,
            Wait
        }

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Duck ||
                   entity.type == NPCID.DuckWhite ||
                   entity.type == NPCID.Grebe ||
                   entity.type == NPCID.Seagull;
        }


        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);
            npc.ai[0] = (float)ActionState.Walk;
            npc.ai[1] = 0;
            npc.ai[2] = 0;
        }

        /// <summary>
        /// Adapted custom Waterfowl AI for vanilla ducks 
        /// </summary>
        /// <param name="npc"></param>
        public override bool PreAI(NPC npc)
        {

            // distances relative to player
            const float avoidDistance = 150f;
            const float spookDistance = 100f;
            const float corneredDistance = avoidDistance + 50f;

            if (Main.netMode == NetmodeID.MultiplayerClient && npc.ai[2] == 0) // random next dir 
            {
                npc.ai[2] = Main.rand.NextBool() ? 1f : -1f;
                npc.netUpdate = true;
            }

            npc.TargetClosest();
            // if touching water, falling or player is really close, transform to flying regardless of ai state 
            if (Main.netMode != NetmodeID.MultiplayerClient &&
               (npc.velocity.Y > 4f || npc.velocity.Y < -4f || npc.wet || Main.player[npc.target].Distance(npc.Center) < spookDistance))
            {
                int direction = npc.direction;

                npc.Transform(npc.type + 1);

                npc.TargetClosest();
                npc.direction = direction;
                npc.netUpdate = true;
            }

            switch (npc.ai[0]) // AI_State
            {
                case (float)ActionState.Wait:

                    npc.ai[1]++;

                    // start walking after a few seconds or when player is close 
                    if (Main.netMode != NetmodeID.MultiplayerClient &&
                       (npc.ai[1] >= Main.rand.Next(240, 480) || // (4sec <-> 8sec) 
                       Main.player[npc.target].Distance(npc.Center) < avoidDistance))
                    {
                        npc.ai[0] = (float)ActionState.Walk; // transition to walk state 

                        npc.ai[1] = 0; //reset AI timer  
                        npc.ai[2] = Main.rand.NextBool() ? 1f : -1f; // next random direction 

                        npc.netUpdate = true;
                    }
                    break;

                case (float)ActionState.Walk:

                    npc.ai[1]++;

                    if (Main.player[npc.target].Distance(npc.Center) >= avoidDistance)
                    {
                        npc.direction = (int)npc.ai[2]; // if player is far enough, pick direction randomly 
                    }
                    else
                    {
                        int direction = npc.direction;
                        npc.TargetClosest(true);         // else, away from closest target 
                        npc.direction = direction * -1;
                        npc.ai[2] = npc.direction;

                    }

                    if (npc.collideX)
                    {

                        Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY);

                        if (npc.velocity.X == 0)
                        {
                            if (Main.player[npc.target].Distance(npc.Center) >= corneredDistance)
                            {
                                npc.ai[2] *= -1;                  // reverse direction if colliding with a block 
                                npc.direction = (int)npc.ai[2];
                            }
                            else
                            {
                                npc.Transform(npc.type + 1);      // fly away if cornered by player on collision
                            }
                        }
                    }

                    npc.velocity.X = 1 * npc.direction;

                    if (Main.netMode != NetmodeID.MultiplayerClient && npc.ai[1] >= Main.rand.Next(180, 420))  // (3sec <-> 7sec) 
                    {
                        npc.velocity.X = 0;

                        npc.ai[0] = (float)ActionState.Wait;
                        npc.ai[1] = 0;

                        npc.netUpdate = true;
                    }
                    break;
            }

            return false;  // omit vanilla AI 
        }


    }
}
