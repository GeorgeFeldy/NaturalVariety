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

		public const int wadingFrameCount = 2;

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
			NPC.width = 22;
			NPC.height = 26;
			NPC.aiStyle = -1;
			AnimationType = NPCID.Duck;

			// Banner = Item.NPCtoBanner(NPCID.Duck);
			// BannerItem = Item.BannerToItem(Banner);

			AI_State = 0;
			AI_Timer = 0;
			AI_NextDir = 0;
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

