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

		public const int wadingFrameCount = 13; // 1 idle, 8 walikng, 4 flying  

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

			// pick a random direction initally 
			if (AI_NextDir == 0 && Main.netMode != NetmodeID.MultiplayerClient)
			{
				AI_NextDir = Main.rand.NextBool() ? 1f : -1f;
				NPC.netUpdate = true;
			}

			AI_Timer++;

			// underwater while idle or walking?
			bool walkingUnderwater = false;
			if(AI_State == (float)ActionState.Wait || AI_State == (float)ActionState.Walk)
            {
				int walkingCenterX = (int)((NPC.position.X + (float)(NPC.width / 2)) / 16f) + NPC.direction;
				int walkingCenterY = (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f);

				walkingUnderwater = Main.tile[walkingCenterX, walkingCenterY - 1].LiquidAmount > 0 &&
									Main.tile[walkingCenterX, walkingCenterY    ].LiquidAmount > 0 &&
									Main.tile[walkingCenterX, walkingCenterY + 1].LiquidAmount > 0;
			}
			switch (AI_State)
			{
				case (float)ActionState.Wait:

					NPC.noGravity = false;
					NPC.width = 38;
					NPC.height = 58;

					NPC.TargetClosest();

					if (Main.netMode != NetmodeID.MultiplayerClient &&
					  (AI_Timer >= Main.rand.Next(240, 480) || // (4sec <-> 8sec) 
					  Main.player[NPC.target].Distance(NPC.Center) < avoidDistance)) // TODO: adjust distance based on critter friendliness
					{

						if(NPC.velocity.Y > 4f ||
							NPC.velocity.Y < -4f ||
							NPC.life < NPC.lifeMax ||
							walkingUnderwater ||
							Main.player[NPC.target].Distance(NPC.Center) < spookDistance)
						{
							AI_State = (float)ActionState.Fly;
							AI_Timer = 0;
							NPC.direction = (int)AI_NextDir;
							NPC.velocity.X = 4f * NPC.direction;
							AI_NextDir = Main.rand.NextBool() ? 1f : -1f;
						}
						else
                        {
							AI_State = (float)ActionState.Walk;
							AI_Timer = 0;
							AI_NextDir = Main.rand.NextBool() ? 1f : -1f;
						}
					
						NPC.netUpdate = true;
					}
					break;

				case (float)ActionState.Walk:

					NPC.noGravity = false;
					NPC.width = 38;
					NPC.height = 58;

					NPC.TargetClosest();

					

					if (NPC.velocity.Y > 4f || 
						NPC.velocity.Y < -4f || 
						walkingUnderwater || 
						NPC.life < NPC.lifeMax ||
						Main.player[NPC.target].Distance(NPC.Center) < spookDistance)
                    {
						AI_State = (float)ActionState.Fly;
						AI_Timer = 0;
					}

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
					if (NPC.collideX) 
					{

						Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
						
						if(NPC.velocity.X == 0)
                        {
							if (Main.player[NPC.target].Distance(NPC.Center) >= corneredDistance)
							{
								AI_NextDir *= -1;                // reverse direction if colliding with a block 
								NPC.direction = (int)AI_NextDir;
							}
							else
							{
								AI_State = (float)ActionState.Fly;
								AI_Timer = 0;
								NPC.direction = (int)AI_NextDir;
								NPC.velocity.X = 4f * NPC.direction;
							}
						}
						
					}

					NPC.velocity.X = 1.25f * (float)NPC.direction;

					if (Main.netMode != NetmodeID.MultiplayerClient && AI_Timer >= Main.rand.Next(180, 420)) // (3sec <-> 7sec) 
					{
						NPC.velocity.X = 0;
						AI_State = (float)ActionState.Wait;
						AI_Timer = 0;

						NPC.netUpdate = true;
					}
					break;


				case (float)ActionState.Fly: // from vanilla Duck AI 

					NPC.width = 90;
					NPC.height = 40;
					NPC.noGravity = true;

					if (Main.player[NPC.target].dead)
						return;

					bool attemptLand = false;
					AI_Timer += 1f;
					if (AI_Timer >= 600f)
						attemptLand = true;

					int flyingCenterX = (int)((NPC.position.X + (float)(NPC.width / 2)) / 16f) + NPC.direction;
					int flyingCenterY = (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f);
					int belowY = (int)((NPC.position.Y + 60) / 16f); // offset is non-flying height 

					bool tileBelowSolid = (Main.tile[flyingCenterX, belowY].HasUnactuatedTile && Main.tileSolid[Main.tile[flyingCenterX, belowY].TileType]);

					bool aboveDeepWater = Main.tile[flyingCenterX, flyingCenterY    ].LiquidAmount > 0 &&
										  Main.tile[flyingCenterX, flyingCenterY + 1].LiquidAmount > 0 &&
										  Main.tile[flyingCenterX, flyingCenterY + 2].LiquidAmount > 0   ;

					if (attemptLand)
					{

                        if (aboveDeepWater) // restart flight if center is submerged in liquid 
                        {
							AI_Timer = 0;
							return;
						}

						if (NPC.velocity.Y == 0f || tileBelowSolid)
						{
							NPC.velocity.X = 0f;
							NPC.velocity.Y = 0f;
							NPC.ai[0] = 0f;
							AI_Timer = 0f;
							if (Main.netMode != NetmodeID.MultiplayerClient)
							{

								int direction5 = NPC.direction;

								AI_State = (float)ActionState.Walk;

								NPC.TargetClosest();
								NPC.direction = direction5;
								AI_Timer = 200 + Main.rand.Next(200);

								NPC.netUpdate = true;
							}
						}
						else
						{
							NPC.velocity.X *= 0.98f;
							NPC.velocity.Y += 0.1f;
							if (NPC.velocity.Y > 2f)
								NPC.velocity.Y = 2f;
						}

						return;
					}

					if (NPC.collideX)
					{
						NPC.direction *= -1;
						NPC.velocity.X = NPC.oldVelocity.X * -0.5f;
						if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
							NPC.velocity.X = 2f;

						if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
							NPC.velocity.X = -2f;
					}

					if (NPC.collideY)
					{
						NPC.velocity.Y = NPC.oldVelocity.Y * -0.5f;
						if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f)
							NPC.velocity.Y = 1f;

						if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f)
							NPC.velocity.Y = -1f;
					}

					if (NPC.direction == -1 && NPC.velocity.X > -3f)
					{
						NPC.velocity.X -= 0.1f;
						if (NPC.velocity.X > 3f)
							NPC.velocity.X -= 0.1f;
						else if (NPC.velocity.X > 0f)
							NPC.velocity.X -= 0.05f;

						if (NPC.velocity.X < -3f)
							NPC.velocity.X = -3f;
					}
					else if (NPC.direction == 1 && NPC.velocity.X < 3f)
					{
						NPC.velocity.X += 0.1f;
						if (NPC.velocity.X < -3f)
							NPC.velocity.X += 0.1f;
						else if (NPC.velocity.X < 0f)
							NPC.velocity.X += 0.05f;

						if (NPC.velocity.X > 3f)
							NPC.velocity.X = 3f;
					}

					int num1038 = (int)((NPC.position.X + (float)(NPC.width / 2)) / 16f) + NPC.direction;
					int num1039 = (int)((NPC.position.Y + (float)NPC.height) / 16f);
					bool flag59 = true;
					int num1040 = 15;
					bool flag60 = false;
					for (int num1041 = num1039; num1041 < num1039 + num1040; num1041++)
					{
						// if (Main.tile[num1038, num1041] == null)
						// 	Main.tile[num1038, num1041] = new Tile();

						if ((Main.tile[num1038, num1041].HasUnactuatedTile && Main.tileSolid[Main.tile[num1038, num1041].TileType]) || Main.tile[num1038, num1041].LiquidAmount > 0)
						{
							if (num1041 < num1039 + 5)
								flag60 = true;

							flag59 = false;
							break;
						}
					}

					if (flag59)
						NPC.velocity.Y += 0.1f;
					else
						NPC.velocity.Y -= 0.1f;

					if (flag60)
						NPC.velocity.Y -= 0.2f;

					if (NPC.velocity.Y > 3f)
						NPC.velocity.Y = 3f;

					if (NPC.velocity.Y < -4f)
						NPC.velocity.Y = -4f;

					break;

			}
		}

        public override void FindFrame(int frameHeight)
        {


			NPC.spriteDirection = NPC.direction;

			switch (AI_State)
            {
				case (float)ActionState.Wait:

					NPC.frameCounter = 0;
					NPC.frame.Y = 0 * frameHeight; // frame 1 idle 

					break;

				case (float)ActionState.Walk:

					NPC.frameCounter++;

					if(NPC.frameCounter < 70) // 7 walking frames (without frame 2)
                    {
						NPC.frame.Y = ((int)NPC.frameCounter / 10 + 2) * frameHeight;  // (70 game frames / 10) + 2 frame offset 
					}
					else 
						NPC.frameCounter = 0;

					break;

				case (float)ActionState.Fly:

					NPC.frameCounter++;
					if (NPC.frameCounter < 20) // 4 flying frames 
					{
						NPC.frame.Y = ((int)NPC.frameCounter / 5 + 9) * frameHeight; // (20 game frames / 5) + 9 frame offset   

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
				int legsGoreType;

				string className = this.GetType().Name;

				try
				{
					headGoreType = Mod.Find<ModGore>(className + "_Gore_Head").Type;
					bodyGoreType = Mod.Find<ModGore>(className + "_Gore_Body").Type;
					wingGoreType = Mod.Find<ModGore>(className + "_Gore_Wing").Type;
					legsGoreType = Mod.Find<ModGore>(className + "_Gore_Legs").Type;
				}
				catch
				{
					// TODO: check gore ID 0 
					headGoreType = 555;	
					bodyGoreType = 556;	
					wingGoreType = 557;	
					legsGoreType = 577; 
				}

				var entitySource = NPC.GetSource_Death();

				for (int idx = 0; idx < 10; idx++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2 * hitDirection, -2f);
				}

				Gore.NewGore(entitySource, NPC.position, NPC.velocity, headGoreType);
				Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y), NPC.velocity, bodyGoreType);
				Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y), NPC.velocity, wingGoreType);
				Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y), NPC.velocity, legsGoreType);
			}
		}

	}
}

