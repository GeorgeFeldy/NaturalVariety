using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;


namespace NaturalVariety.NPCs.Critters
{
	/// <summary>
	/// Generic class for all wading birds in this mod 
	/// </summary>
	public abstract class WadingBird : Critter
	{

		// AI action state 
		private enum ActionState
		{
			Walk,
			Wait,
			Fly
		}

		public const int wadingFrameCount = 10; // walikng only 

		public ref float AI_State => ref NPC.ai[0];
		public ref float AI_Timer => ref NPC.ai[1];
		public ref float AI_NextDir => ref NPC.ai[2];

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.npcFrameCount[Type] = wadingFrameCount;

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
			NPC.width = 38;
			NPC.height = 58;
			NPC.aiStyle = -1;

			// Banner = Item.NPCtoBanner(NPCID.Duck);
			// BannerItem = Item.BannerToItem(Banner);

			AI_State = 0;
			AI_Timer = 0;
			AI_NextDir = 0;
		}

        public override void AI()
        {
			// distances relative to player
			const float avoidDistance = 150f;
			const float spookDistance = 100f;
			const float corneredDistance = avoidDistance + 50f;

			if (AI_NextDir == 0 && Main.netMode != NetmodeID.MultiplayerClient)
			{
				AI_NextDir = Main.rand.NextBool() ? 1f : -1f;
				NPC.netUpdate = true;
			}

			NPC.TargetClosest();
			// if touching water, falling or player is really close, transform to flying regardless of action state 
			if (Main.netMode != NetmodeID.MultiplayerClient &&
			   (NPC.velocity.Y > 4f || NPC.velocity.Y < -4f || NPC.wet || Main.player[NPC.target].Distance(NPC.Center) < spookDistance))
			{
				int direction = NPC.direction;


				NPC.TargetClosest();
				NPC.direction = direction;
				NPC.netUpdate = true;
				return;
			}

			switch (AI_State)
			{
				case (float)ActionState.Wait:

					AI_Timer++;

					// start walking after a few seconds or when player is close 
					if (Main.netMode != NetmodeID.MultiplayerClient &&
					  (AI_Timer >= Main.rand.Next(240, 480) || // (4sec <-> 8sec) 
					  Main.player[NPC.target].Distance(NPC.Center) < avoidDistance)) // TODO: adjust distance based on critter friendliness
					{
						AI_State = (float)ActionState.Walk;
						AI_Timer = 0;
						AI_NextDir = Main.rand.NextBool() ? 1f : -1f;

						NPC.netUpdate = true;
					}
					break;

				case (float)ActionState.Walk:

					AI_Timer++;

					if (Main.player[NPC.target].Distance(NPC.Center) >= avoidDistance)
					{
						NPC.direction = (int)AI_NextDir;     // if player is far enough, pick direction randomly 
					}
					else
					{
						int direction = NPC.direction;
						NPC.TargetClosest(true);             // else, away from closest target 
						NPC.direction = direction * -1;
						AI_NextDir = (float)NPC.direction;

					}
					if (NPC.collideX) // TODO: replace with only two-block high collisions
					{
						if (Main.player[NPC.target].Distance(NPC.Center) >= corneredDistance)
						{
							AI_NextDir *= -1;                // reverse direction if colliding with a block 
							NPC.direction = (int)AI_NextDir;
						}
						//else
						//{
						//	ConvertToFlying();              // fly away if cornered by player on collision
						//}
					}

					NPC.velocity.X = 1 * NPC.direction;

					if (Main.netMode != NetmodeID.MultiplayerClient && AI_Timer >= Main.rand.Next(180, 420)) // (3sec <-> 7sec) 
					{
						NPC.velocity.X = 0;
						AI_State = (float)ActionState.Wait;
						AI_Timer = 0;

						NPC.netUpdate = true;
					}
					break;
			}
		}

        public override void FindFrame(int frameHeight)
        {
            switch (AI_State)
            {
				case (float)ActionState.Wait:
					NPC.frame.Y = 0;
					break;

				case (float)ActionState.Walk:

					NPC.frameCounter++;

					if(NPC.frameCounter < 80)
                    {
						NPC.frame.Y = ((int)NPC.frameCounter / 8) * frameHeight;
                    }
					else 
						NPC.frameCounter = 0;

					break;
            }
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

			if (NPC.life <= 0)
			{

				int headGoreType;
				int bodyGoreType;
				int wingGoreType;

				string className = this.GetType().Name;

				try
				{
					headGoreType = Mod.Find<ModGore>(className + "_Gore_Head").Type;
					bodyGoreType = Mod.Find<ModGore>(className + "_Gore_Body").Type;
					wingGoreType = Mod.Find<ModGore>(className + "_Gore_Wing").Type;
				}
				catch
				{
					headGoreType = 555;
					bodyGoreType = 556;
					wingGoreType = 557;
				}

				var entitySource = NPC.GetSource_Death();

				for (int idx = 0; idx < 10; idx++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2 * hitDirection, -2f);
				}

				Gore.NewGore(entitySource, NPC.position, NPC.velocity, headGoreType);
				Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y), NPC.velocity, bodyGoreType);
				Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y), NPC.velocity, wingGoreType);
			}
		}

	}
}
