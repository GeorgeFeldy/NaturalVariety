
using NaturalVariety.Utils;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace NaturalVariety.NPCs.Critters
{
    public class FlamingoSpawner : BaseWadingBird
    {

        public override string Texture => NaturalVariety.EmptySprite;

        private enum FlamingoTypes
        {
            PinkFlamingo,
            WhiteFlamingo,
            GoldFlamingo
        }

        private int numberOfFlamingoTypes = Enum.GetNames(typeof(FlamingoTypes)).Length;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers hide = new(0) { Hide = true };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, hide);
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;

            NPC.width = 16;
            NPC.height = 16;

            NPC.lifeMax = 5;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnHelper.FlamingoChance(spawnInfo); ;
        }

        public override bool PreAI()
        {

            NPC.TargetClosest();

            WeightedRandom<int> flockSize = new(Main.rand.Next(1, 5));
            flockSize.Add(1, 2); // 2/5 to be single 
            for (int i = 2; i <= 5; i++)
            {
                flockSize.Add(i, 3); // 3/5 to be multiple (2 to 5)
            }

            int flamingoType = ModContent.NPCType<FlamingoPink>(); // default type

            WeightedRandom<int> flamingoTypePicker = new(Main.rand.Next(numberOfFlamingoTypes));
            flamingoTypePicker.Add((int)FlamingoTypes.PinkFlamingo, 1);
            flamingoTypePicker.Add((int)FlamingoTypes.WhiteFlamingo, 1);


            for (int i = 0; i < flockSize; i++)
            {

                if (Main.player[NPC.target].RollLuck(NPC.goldCritterChance) == 0)
                {
                    flamingoType = ModContent.NPCType<FlamingoGold>();
                }
                else
                {
                    switch (flamingoTypePicker)
                    {
                        case (int)FlamingoTypes.PinkFlamingo: flamingoType = ModContent.NPCType<FlamingoPink>(); break;
                        case (int)FlamingoTypes.WhiteFlamingo: flamingoType = ModContent.NPCType<FlamingoWhite>(); break;
                    }
                }

                int index = NPC.NewNPC(new EntitySource_SpawnNPC(), (int)NPC.position.X + Main.rand.Next(-30, NPC.width + 30), (int)NPC.position.Y + NPC.height,
                  flamingoType);

                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                    NetMessage.SendData(MessageID.SyncNPC, number: index);

            }
            NPC.active = false;
            return false; // avoid call to WadingBird AI 
        }

        public override void FindFrame(int frameHeight)
        {
            // do nothing
        }




    }

}

