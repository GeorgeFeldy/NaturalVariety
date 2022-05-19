using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NaturalVariety.NPCs.Critters;



namespace NaturalVariety.Mechanics
{
    public class ImproveVanillaDuckAI : GlobalNPC
    {

		private enum ActionState
		{
			Walk,
			Wait
		}

		public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return (entity.type == NPCID.Duck || entity.type == NPCID.DuckWhite);
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
        public override void AI(NPC npc)
        {

			if (npc.ai[2] == 0) // random next dir 
			{
				npc.ai[2] = Main.rand.NextBool() ? 1f : -1f;
			}

			npc.TargetClosest();
			// if touching water, falling or player is really close, transform to flying regardless of action state 
			if (Main.netMode != NetmodeID.MultiplayerClient &&
			   (npc.velocity.Y > 4f || npc.velocity.Y < -4f || npc.wet || Main.player[npc.target].Distance(npc.Center) < 100f))
			{
				int direction = npc.direction;

				npc.Transform(npc.type + 1);

				npc.TargetClosest();
				npc.direction = direction;
				npc.netUpdate = true;

				return;
			}


			
			switch (npc.ai[0]) // AI_State
			{


				// case (float)ActionState.Wait:
				case 0:

					npc.ai[1]++; 

					// start walking after a few seconds or when player is close 
					if (npc.ai[1] >= Main.rand.Next(240, 480) || // (4sec <-> 8sec) 
					Main.player[npc.target].Distance(npc.Center) < 200f)
					{
						//npc.ai[0] = (float)ActionState.Walk; // transition to walk state 
						npc.ai[0] = 1;

						npc.ai[1] = 0; //reset AI timer  
						npc.ai[2] = Main.rand.NextBool() ? 1f : -1f; // next random direction 
						Main.NewText("State is " + npc.ai[0].ToString(), 50, 125, 255);
					}
					break;

				case 1:

					npc.ai[1]++;

					if (Main.player[npc.target].Distance(npc.Center) >= 200f)
					{
						npc.direction = (int)npc.ai[2]; // if player is far enough, pick direction randomly 
					}
					else
					{
						int direction = npc.direction;
						npc.TargetClosest(true);       // else, away from closest target 
						npc.direction = direction * -1;
						npc.ai[2] = (float)npc.direction;

					}
					npc.velocity.X = 1 * npc.direction;

					if (npc.ai[1] >= Main.rand.Next(180, 420))
					{ // (3sec <-> 7sec) 
						npc.velocity.X = 0;

						//npc.ai[0] = (float)ActionState.Wait;
						npc.ai[0] = 0;

						npc.ai[1] = 0;
						Main.NewText("State is " + npc.ai[0].ToString(), 50, 125, 255);
					}
					break;
			}

		}

    }
}
