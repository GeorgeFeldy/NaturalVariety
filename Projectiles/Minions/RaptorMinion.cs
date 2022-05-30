using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using NaturalVariety.Items.Weapons.Summons;
using NaturalVariety.Buffs.Minions;


namespace NaturalVariety.Projectiles.Minions
{
    public class RaptorMinion : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raptor");
			// Sets the amount of frames this minion has on its spritesheet
			Main.projFrames[Projectile.type] = 5;
			// This is necessary for right-click targeting
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

			Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
		}

		public sealed override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.tileCollide = false; // Makes the minion go through tiles freely

			// These below are needed for a minion weapon
			Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
			Projectile.minion = true; // Declares this as a minion (has many effects)
			Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
			Projectile.minionSlots = 1f; // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
			Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles
		}

		// Here you can decide if your minion breaks things like grass or pots
		public override bool? CanCutTiles()
		{
			return false;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		public override bool MinionContactDamage()
		{
			return true;
		}


        public override void AI()
        {

			Player owner = Main.player[Projectile.owner];

			if (!CheckActive(owner))
			{
				return;
			}

			if (Main.dayTime)
            {
				DayAI(owner);

            }
			else 
            {
				NightAI(owner);
            }
        }

        private void NightAI(Player owner)
        {

			// for each projectile
			for (int projIdx = 0; projIdx < Main.maxProjectiles; projIdx++)
			{
				Projectile other = Main.projectile[projIdx];

				// for every other minion of the same type, owned by the same player, if they overlap, move them 
				if (projIdx != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < (float)Projectile.width)
				{
					if (Projectile.position.X < other.position.X)
						Projectile.velocity.X -= 0.05f;
					else
						Projectile.velocity.X += 0.05f;

					if (Projectile.position.Y < other.position.Y)
						Projectile.velocity.Y -= 0.05f;
					else
						Projectile.velocity.Y += 0.05f;
				}
			}

			Vector2 projectileCenter = Projectile.position;
			float distfromTarget = 900f;
			bool foundTarget = false;

			int maxPlrDist = 500;
			if (Projectile.ai[1] != 0f || Projectile.friendly)
				maxPlrDist = 1400;

			// if distance between player and minion is greater than maxPlrDist
			if (Math.Abs(Projectile.Center.X - Main.player[Projectile.owner].Center.X) + Math.Abs(Projectile.Center.Y - Main.player[Projectile.owner].Center.Y) > (float)maxPlrDist)
				Projectile.ai[0] = 1f;

			if (owner.HasMinionAttackTargetNPC)
			{
				Projectile.tileCollide = true;
				NPC targetNPC = Projectile.OwnerMinionAttackTargetNPC;

				float distBetween = Vector2.Distance(targetNPC.Center, Projectile.Center);

				if (targetNPC.CanBeChasedBy(this))
					{
						if (distBetween < distfromTarget && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, targetNPC.position, targetNPC.width, targetNPC.height))
						{
							distfromTarget = distBetween;
							projectileCenter = targetNPC.Center;
							foundTarget = true;
						}
					}

				if (!foundTarget)
				{
					for (int npcIdx = 0; npcIdx < 200; npcIdx++)
					{

						targetNPC = Main.npc[npcIdx];

						if (Main.npc[npcIdx].CanBeChasedBy(this))
						{
							float between = Vector2.Distance(targetNPC.Center, Projectile.Center);
							bool inRange = between < distfromTarget;

							if (inRange && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[npcIdx].position, Main.npc[npcIdx].width, Main.npc[npcIdx].height))
								{
									distfromTarget = between;
									projectileCenter = targetNPC.Center;
									foundTarget = true;
								}
							}
					}
				}
			}
			else
			{
				Projectile.tileCollide = false;
			}

			if (!foundTarget)
			{
				Projectile.friendly = true;

				float projSpeed;
				float inertia;

				Vector2 vectorToIdlePosition = owner.Center;
				vectorToIdlePosition.Y -= 60f; // adjust a few coords

				vectorToIdlePosition -= Projectile.Center;

				float distanceToIdlePosition = vectorToIdlePosition.Length();

				if (distanceToIdlePosition < 100f)
				{
					projSpeed = 8f;
				} 
				else
				{
					projSpeed = 12f;
				}

				if (distanceToIdlePosition > 2000f)
				{	
					// teleport to player 
					Projectile.position.X = owner.Center.X - (float)(Projectile.width / 2);
					Projectile.position.Y = owner.Center.Y - (float)(Projectile.width / 2);
				}

				if (distanceToIdlePosition > 70f)
				{
					distanceToIdlePosition = projSpeed / distanceToIdlePosition;
					Projectile.velocity.X = (Projectile.velocity.X * 20f + Projectile.position.X * distanceToIdlePosition) / 21f;
					Projectile.velocity.Y = (Projectile.velocity.Y * 20f + Projectile.position.Y * distanceToIdlePosition) / 21f;
				}
				else
				{
					if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
					{
						Projectile.velocity.X = -0.15f;
						Projectile.velocity.Y = -0.05f;
					}

					Projectile.velocity *= 1.01f;
				}

				Projectile.friendly = false;
				Projectile.rotation = Projectile.velocity.X * 0.05f;
				Projectile.frameCounter++;
				if (Projectile.frameCounter >= 4)
				{
					Projectile.frameCounter = 0;
					Projectile.frame++;
				}

				if (Projectile.frame > 3)
					Projectile.frame = 0;

				if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
					Projectile.spriteDirection = -Projectile.direction;

				return;
			}

			if (Projectile.ai[1] == -1f)
				Projectile.ai[1] = 17f;

			if (Projectile.ai[1] > 0f)
				Projectile.ai[1] -= 1f;

			if (Projectile.ai[1] == 0f)
			{
				Projectile.friendly = true;
				float num476 = 16f;
				float num477 = distfromTarget - Projectile.Center.X;
				float num478 = distfromTarget - Projectile.Center.Y;
				float num479 = (float)Math.Sqrt(num477 * num477 + num478 * num478);

				if (num479 < 100f)
					num476 = 10f;

				num479 = num476 / num479;
				num477 *= num479;
				num478 *= num479;
				Projectile.velocity.X = (Projectile.velocity.X * 14f + num477) / 15f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 14f + num478) / 15f;
			}
			else
			{
				Projectile.friendly = false;
				if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 10f)
					Projectile.velocity *= 1.05f;
			}

			Projectile.rotation = Projectile.velocity.X * 0.05f;
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;
			}

			if (Projectile.frame < 4)
				Projectile.frame = 4;

			if (Projectile.frame > 7)
				Projectile.frame = 4;

			if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
				Projectile.spriteDirection = -Projectile.direction;
		}


			// The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
			private void DayAI(Player owner)
			{
				GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
				SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
				Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
				Visuals();
			}

			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			private bool CheckActive(Player owner)
			{
				if (owner.dead || !owner.active)
				{
					owner.ClearBuff(ModContent.BuffType<RaptorMinionBuff>());

					return false;
				}

				if (owner.HasBuff(ModContent.BuffType<RaptorMinionBuff>()))
				{
					Projectile.timeLeft = 2;
				}

				return true;
			}

		private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
		{
			Vector2 idlePosition = owner.Center;
			idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

			// If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
			// The index is projectile.minionPos
			float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
			idlePosition.X += minionPositionOffsetX; // Go behind the player

			// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

			// Teleport to player if distance is too big
			vectorToIdlePosition = idlePosition - Projectile.Center;
			distanceToIdlePosition = vectorToIdlePosition.Length();

			if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
			{
				// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
				// and then set netUpdate to true
				Projectile.position = idlePosition;
				Projectile.velocity *= 0.1f;
				Projectile.netUpdate = true;
			}

			// If your minion is flying, you want to do this independently of any conditions
			float overlapVelocity = 0.04f;

			// Fix overlap with other minions
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile other = Main.projectile[i];

				if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
				{
					if (Projectile.position.X < other.position.X)
					{
						Projectile.velocity.X -= overlapVelocity;
					}
					else
					{
						Projectile.velocity.X += overlapVelocity;
					}

					if (Projectile.position.Y < other.position.Y)
					{
						Projectile.velocity.Y -= overlapVelocity;
					}
					else
					{
						Projectile.velocity.Y += overlapVelocity;
					}
				}
			}
		}

		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
		{
			// Starting search distance
			distanceFromTarget = 700f;
			targetCenter = Projectile.position;
			foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (owner.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[owner.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, Projectile.Center);

				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 2000f)
				{
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}

			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, Projectile.Center);
						bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;

						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			Projectile.friendly = foundTarget;
		}

		private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
		{
			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 20f;

			if (foundTarget)
			{
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f)
				{
					// The immediate range around the target (so it doesn't latch onto it when close)
					Vector2 direction = targetCenter - Projectile.Center;
					direction.Normalize();
					direction *= speed;

					Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
				}
			}
			else
			{
				// Minion doesn't have a target: return to player and idle
				if (distanceToIdlePosition > 600f)
				{
					// Speed up the minion if it's away from the player
					speed = 12f;
					inertia = 60f;
				}
				else
				{
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}

				if (distanceToIdlePosition > 20f)
				{
					// The immediate range around the player (when it passively floats about)

					// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (Projectile.velocity == Vector2.Zero)
				{
					// If there is a case where it's not moving at all, give it a little "poke"
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
			}
		}

		private void Visuals()
		{
			// So it will lean slightly towards the direction it's moving
			Projectile.rotation = Projectile.velocity.X * 0.05f;

			// This is a simple "loop through all frames from top to bottom" animation
			int frameSpeed = 5;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed)
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;

				if (Projectile.frame >= Main.projFrames[Projectile.type])
				{
					Projectile.frame = 0;
				}
			}

			// Some visuals here
			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
		}
		}
	}

