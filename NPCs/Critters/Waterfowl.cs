using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using NaturalVariety.Utils;


// TODO: Eventually, merge the two classes with full custom AI  


namespace NaturalVariety.NPCs.Critters
{
	/// <summary>
	/// Generic class for all walking duck-like critters in this mod 
	/// </summary>
	public abstract class Waterfowl : Critter
	{

		// AI action state 
		private enum ActionState
		{
			Walk,
			Wait
		}

		public ref float AI_State => ref NPC.ai[0];
		public ref float AI_Timer => ref NPC.ai[1];
		public ref float AI_NextDir => ref NPC.ai[2];

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Duck];

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
			NPC.width = 22;
			NPC.height = 26;                                                           
			NPC.aiStyle = -1;
			AnimationType = NPCID.Duck;

			AI_State = 0;
			AI_Timer = 0;
			AI_NextDir = 0;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnHelper.DuckChance(spawnInfo);
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

		/// <summary>
		/// Adaption of Passive AI Duck-type NPC
		/// </summary>
		public override void AI()
		{
			// distances relative to player
			const float avoidDistance = 150f;
			const float spookDistance = 100f;
			const float corneredDistance = avoidDistance + 50f;

			if(AI_NextDir == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
				AI_NextDir = Main.rand.NextBool() ? 1f : -1f;
				NPC.netUpdate = true;
			}

			NPC.TargetClosest();
			AI_Timer++;


			// if touching water, falling or player is really close, transform to flying regardless of action state 
			if (Main.netMode != NetmodeID.MultiplayerClient && 
			   (NPC.velocity.Y > 4f || NPC.velocity.Y < -4f || NPC.wet || Main.player[NPC.target].Distance(NPC.Center) < spookDistance))
            {
				int direction = NPC.direction;

				ConvertToFlying(); // convert to corresponding "Duck AI" WaterfowlFly child type NPC 

				NPC.TargetClosest();
				NPC.direction = direction;
				NPC.netUpdate = true;
				return;
			}

			switch (AI_State)
            {
				case (float)ActionState.Wait:


					// start walking after a few seconds or when player is close 
					if(Main.netMode != NetmodeID.MultiplayerClient && 
					  (AI_Timer >= Main.rand.Next(240,480)		   || // (4sec <-> 8sec) 
					  Main.player[NPC.target].Distance(NPC.Center) < avoidDistance)) // TODO: adjust distance based on critter friendliness
					{
						AI_State = (float)ActionState.Walk;
						AI_Timer = 0;
						AI_NextDir = Main.rand.NextBool() ? 1f : -1f;

						NPC.netUpdate = true;
					}
					break;

				case (float)ActionState.Walk:

					if(Main.player[NPC.target].Distance(NPC.Center) >= avoidDistance)
                    {
						NPC.direction = (int)AI_NextDir;     // if player is far enough, pick direction randomly 
					}
					else
                    {
						int direction = NPC.direction;
						NPC.TargetClosest(true);	         // else, away from closest target 
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
								ConvertToFlying();              // fly away if cornered by player on collision
							}
						}
					
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
		/// <summary>
		/// convert to corresponding "Duck AI" WaterfowlFly child type NPC 
		/// </summary>
		public void ConvertToFlying()
		{
			string nextTypeString = this.GetType().Name + "Fly";
			Type nextType = System.Type.GetType("NaturalVariety.NPCs.Critters." + nextTypeString);
			var func = typeof(ModContent).GetMethod("NPCType");
			var genericFunc = func.MakeGenericMethod(nextType);
			NPC.Transform((int)genericFunc.Invoke(this, null));
		}
	}

	/// <summary>
	/// Generic class for all flying/swimming duck-like critters in this mod 
	/// </summary>
	public abstract class WaterfowlFly : Critter
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

			//display name based on child class name, but drop "Fly" to match Passive AI variant 
			DisplayName.SetDefault((this.GetType().Name).Replace("Fly", ""));

			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Duck2];          

            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)                   
            {
                //Position = new Vector2(3f, 0f),
                //Velocity = 1f,
				Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 28;
            NPC.height = 22;
            NPC.aiStyle = -1;
            AnimationType = NPCID.Duck2;                  

            Banner = Item.NPCtoBanner(NPCID.Duck);
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

			if (NPC.life <= 0)
			{

				int headGoreType;
				int bodyGoreType;
				int wingGoreType;

				string className = (this.GetType().Name).Replace("Fly", "");

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

		/// <summary>
		/// Copy-paste adaption of Vanilla Duck AI (id 68) :) 
		/// TODO: make code more readable with action states and NPC.ai refs
		/// TODO: eventually merge with Waterfowl class
		/// </summary>
		public override void AI()
        {
			NPC.noGravity = true;
			if (NPC.ai[0] == 0f)
			{
				NPC.noGravity = false;
				int direction4 = NPC.direction;
				int num1032 = NPC.target;
				NPC.TargetClosest();
				if (num1032 >= 0 && direction4 != 0)
					NPC.direction = direction4;

				if (NPC.wet)
				{
					float num1033 = 2f;
					NPC.velocity.X = (NPC.velocity.X * 19f + num1033 * (float)NPC.direction) / 20f;
					int num1034 = (int)(NPC.Center.X + (float)((NPC.width / 2 + 8) * NPC.direction)) / 16;
					int num1035 = (int)(NPC.Center.Y / 16f);
					int j4 = (int)(NPC.position.Y / 16f);
					int num1036 = (int)((NPC.position.Y + (float)NPC.height) / 16f);
					// if (Main.tile[num1034, num1035] == null)
					// 	Main.tile[num1034, num1035] = new Tile();
					// 
					// if (Main.tile[num1034, num1036] == null)
					// 	Main.tile[num1034, num1036] = new Tile();

					if (WorldGen.SolidTile(num1034, num1035) || WorldGen.SolidTile(num1034, j4) || WorldGen.SolidTile(num1034, num1036) || Main.tile[num1034, num1036].LiquidAmount == 0)
						NPC.direction *= -1;

					NPC.spriteDirection = NPC.direction;
					if (NPC.velocity.Y > 0f)
						NPC.velocity.Y *= 0.5f;

					NPC.noGravity = true;
					num1034 = (int)(NPC.Center.X / 16f);
					num1035 = (int)(NPC.Center.Y / 16f);
					float num1037 = NPC.position.Y + (float)NPC.height;

					//if (Main.tile[num1034, num1035 - 1] == null)
					//	Main.tile[num1034, num1035 - 1] = new Tile();
					//
					//if (Main.tile[num1034, num1035] == null)
					//	Main.tile[num1034, num1035] = new Tile();
					//
					//if (Main.tile[num1034, num1035 + 1] == null)
					//	Main.tile[num1034, num1035 + 1] = new Tile();

					if (Main.tile[num1034, num1035 - 1].LiquidAmount > 0)
					{
						num1037 = num1035 * 16;
						num1037 -= (float)((int)Main.tile[num1034, num1035 - 1].LiquidAmount / 16);
					}
					else if (Main.tile[num1034, num1035].LiquidAmount > 0)
					{
						num1037 = (num1035 + 1) * 16;
						num1037 -= (float)((int)Main.tile[num1034, num1035].LiquidAmount / 16);
					}
					else if (Main.tile[num1034, num1035 + 1].LiquidAmount > 0)
					{
						num1037 = (num1035 + 2) * 16;
						num1037 -= (float)((int)Main.tile[num1034, num1035 + 1].LiquidAmount / 16);
					}

					num1037 -= 6f;
					if (NPC.Center.Y > num1037)
					{
						NPC.velocity.Y -= 0.1f;
						if (NPC.velocity.Y < -8f)
							NPC.velocity.Y = -8f;

						if (NPC.Center.Y + NPC.velocity.Y < num1037)
							NPC.velocity.Y = num1037 - NPC.Center.Y;
					}
					else
					{
						NPC.velocity.Y = num1037 - NPC.Center.Y;
					}
				}

				if (Main.netMode == NetmodeID.MultiplayerClient)
					return;

				if (!NPC.wet)
				{
					NPC.ai[0] = 1f;
					NPC.netUpdate = true;
					NPC.direction = -NPC.direction;
					return;
				}

				Rectangle rectangle4 = new Rectangle((int)Main.player[NPC.target].position.X, (int)Main.player[NPC.target].position.Y, Main.player[NPC.target].width, Main.player[NPC.target].height);
				if (new Rectangle((int)NPC.position.X - 100, (int)NPC.position.Y - 100, NPC.width + 200, NPC.height + 200).Intersects(rectangle4) || NPC.life < NPC.lifeMax)
				{


					NPC.ai[0] = 1f;
					NPC.velocity.Y -= 6f;
					NPC.netUpdate = true;
					NPC.direction = -NPC.direction;
				}
			}
			else
			{
				if (Main.player[NPC.target].dead)
					return;

				bool flag58 = false;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= 300f)
					flag58 = true;

				if (flag58)
				{
					if (NPC.velocity.Y == 0f || NPC.collideY || NPC.wet)
					{
						NPC.velocity.X = 0f;
						NPC.velocity.Y = 0f;
						NPC.ai[0] = 0f;
						NPC.ai[1] = 0f;
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{

							int direction5 = NPC.direction;

							ConvertToWalking();	

							NPC.TargetClosest();
							NPC.direction = direction5;
							NPC.ai[0] = 0f;
							NPC.ai[1] = 200 + Main.rand.Next(200);

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
			}
		}

		/// <summary>
		///  convert to corresponding Passive AI "Waterfowl" child type NPC
		/// </summary>
		public void ConvertToWalking()
		{
			string nextTypeString = (this.GetType().Name).Replace("Fly", "");
			Type nextType = System.Type.GetType("NaturalVariety.NPCs.Critters." + nextTypeString);
			var func = typeof(ModContent).GetMethod("NPCType");
			var genericFunc = func.MakeGenericMethod(nextType);
			NPC.Transform((int)genericFunc.Invoke(this, null));
		}

	}
}

