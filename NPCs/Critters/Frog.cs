using Microsoft.Xna.Framework;
using NaturalVariety.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace NaturalVariety.NPCs.Critters
{
    /// <summary>
    /// Generic class for all frog type type critters in this mod 
    /// </summary>
    public abstract class Frog : Critter 
    {

        public enum ActionState
        {
            Walk,
            Wait,
            Swim
        }

        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Timer => ref NPC.ai[1];
        public ref float AI_NextDir => ref NPC.ai[2];

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Frog];           

            //NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)                   
            //{
            //    Position = new Vector2(1f, -14f),
            //    Velocity = 0.05f,
            //    PortraitPositionYOverride = -30f
            //};
            //NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);                  
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnHelper.JungleCritterChance(spawnInfo);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 12;
            NPC.height = 10;
            NPC.aiStyle = -1;                                                 
            //AnimationType = NPCID.Frog;                   
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
                return; 
            }

            if(NPC.life <= 0)
            {
                int headGore;
                int legsGore;

                string className = this.GetType().Name;

                try
                {
                    headGore = Mod.Find<ModGore>(className + "_Gore_Head").Type;
                    legsGore = Mod.Find<ModGore>(className + "_Gore_Legs").Type;
                }
                catch
                {
                    headGore = 551;
                    legsGore = 552;
                }

                var entitySource = NPC.GetSource_Death();

                
                Gore.NewGore(entitySource, NPC.position, NPC.velocity, headGore);
                Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y), NPC.velocity, legsGore);
            }
        }

        public override void AI()
        {

            if (AI_NextDir == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                AI_NextDir = Main.rand.NextBool() ? 1f : -1f;
                NPC.netUpdate = true;
            }

            //NPC.TargetClosest();
            AI_Timer++;

            switch (AI_State)
            {
                case (float)ActionState.Wait:

                    if (NPC.wet)
                    {
                        AI_State = (float)ActionState.Swim;
                        AI_Timer = 0;
                    }

                    if (Main.netMode != NetmodeID.MultiplayerClient && (AI_Timer >= Main.rand.Next(240, 480)))
                    {
                        AI_State = (float)ActionState.Walk;
                        AI_Timer = 0;
                        AI_NextDir = Main.rand.NextBool() ? 1f : -1f;

                        NPC.netUpdate = true;
                    }
                    break;

                case (float)ActionState.Walk:

                    if (NPC.wet)
                    {
                        AI_State = (float)ActionState.Swim;
                        AI_Timer = 0;
                    }

					NPC.direction = (int)AI_NextDir; 
			
                    if (NPC.collideX)
                    {


                        int num18 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)(15 * NPC.direction)) / 16f);
                        int num19 = (int)((NPC.position.Y + (float)NPC.height - 16f) / 16f);

                        if (!Collision.SolidTilesVersatile(num18 - NPC.direction * 2, num18 - NPC.direction, num19 - 5, num19 - 1) && !Collision.SolidTiles(num18, num18, num19 - 5, num19 - 2))
                        {
                            NPC.velocity.Y = -6f;
                        }
                        //else
                        //{
                        //    NPC.direction *= -(int)AI_NextDir;
                        //    NPC.velocity *= NPC.direction;
                        //}

                        //Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);

                        //if (NPC.velocity.X == 0)
                        //{
                        //    NPC.velocity.Y = -6f;
                        //    NPC.velocity.X = 0.5f * NPC.direction;
                        //    //
                        //}
                    }

                    NPC.velocity.X = 1 * (float)NPC.direction;

                    if (Main.netMode != NetmodeID.MultiplayerClient && AI_Timer >= Main.rand.Next(180, 420)) // (3sec <-> 7sec) 
                    {
                        NPC.velocity.X = 0;
                        AI_State = (float)ActionState.Wait;
                        AI_Timer = 0;

                        NPC.netUpdate = true;
                    }

                    break;

                case (float)ActionState.Swim:

                    if (!NPC.wet)
                    {
                        AI_State = (float)ActionState.Walk;
                        AI_Timer = 0;
                    }

                    if (NPC.collideX)
                    {

                        Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);

                        if(NPC.velocity.X == 0)
                        {
                            NPC.velocity.Y = -6f;
                            NPC.direction *= -1;
                            NPC.velocity.X *= NPC.direction;
                        }
                    }
                    else if (Math.Abs(NPC.velocity.X) < 0.05f && Math.Abs(NPC.velocity.Y) < 0.05f)
                    {
                        NPC.velocity.X += 10f * (float)NPC.direction;
                    }
                    else
                    {
                        NPC.velocity.X *= 0.9f;
                    }

                    if (Collision.GetWaterLine(NPC.Center.ToTileCoordinates(), out float waterLineHeight))
                    {
                        float num76 = NPC.Center.Y + 1f;
                        if (NPC.Center.Y > waterLineHeight)
                        {
                            NPC.velocity.Y -= 0.8f;
                            if (NPC.velocity.Y < -4f)
                                NPC.velocity.Y = -4f;

                            if (num76 + NPC.velocity.Y < waterLineHeight)
                                NPC.velocity.Y = waterLineHeight - num76;
                        }
                        else
                        {
                            NPC.velocity.Y = MathHelper.Min(NPC.velocity.Y, waterLineHeight - num76);
                        }
                    }
                    else
                    {
                        NPC.velocity.Y -= 0.2f;
                    }

                    break;


            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.wet)
            {
                NPC.frameCounter = 0.0;
                if (NPC.velocity.X > 0.25f || NPC.velocity.X < -0.25f)
                    NPC.frame.Y = frameHeight * 10;
                else if (NPC.velocity.X > 0.15f || NPC.velocity.X < -0.15f)
                    NPC.frame.Y = frameHeight * 11;
                else
                    NPC.frame.Y = frameHeight * 12;
            }
            else if (NPC.velocity.Y == 0f)
            {
                if (NPC.velocity.X == 0f)
                {
                    NPC.frameCounter += 1.0;
                    if (NPC.frameCounter > 6.0)
                    {
                        NPC.frameCounter = 0.0;
                        NPC.frame.Y += frameHeight;
                    }

                    if (NPC.frame.Y > frameHeight * 5)
                        NPC.frame.Y = 0;

                    return;
                }

                NPC.frameCounter += 1.0;
                int num200 = 6;
                if (NPC.frameCounter < (double)num200)
                {
                    NPC.frame.Y = 0;
                    return;
                }

                if (NPC.frameCounter < (double)(num200 * 2))
                {
                    NPC.frame.Y = frameHeight * 6;
                    return;
                }

                if (NPC.frameCounter < (double)(num200 * 3))
                {
                    NPC.frame.Y = frameHeight * 8;
                    return;
                }

                NPC.frame.Y = frameHeight * 9;
                if (NPC.frameCounter >= (double)(num200 * 4 - 1))
                    NPC.frameCounter = 0.0;
            }
            else if (NPC.velocity.Y > 0f)
            {
                 NPC.frame.Y = frameHeight * 9;
            }
            else
            {
                NPC.frame.Y = frameHeight * 8;
            }
        }

    }
}