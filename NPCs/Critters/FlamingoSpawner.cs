
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.DataStructures;

using NaturalVariety.Utils;


namespace NaturalVariety.NPCs.Critters
{
    public class FlamingoSpawner : WadingBird 
    {

        public override string Texture => NaturalVariety.EmptySprite;

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
            return SpawnHelper.FlamingoChance(spawnInfo);
        }

        public override bool PreAI()
        {

            const int numberOfFlamingoes = 2;

            WeightedRandom<int> flockSize = new(Main.rand.Next(1,5));
            flockSize.Add(1, 2); // 2/5 to be single 
            for (int i = 2; i <= 5; i++)
            {
                flockSize.Add(i, 3); // 3/5 to be multiple (2 to 5)
            }

            int flamingoTypePicker; 
            int flamingoType = ModContent.NPCType<Flamingo>();

            for (int i = 0; i < flockSize; i++)
            {

                flamingoTypePicker = Main.rand.Next(numberOfFlamingoes) + 1;

                switch (flamingoTypePicker)
                {
                    case 1: flamingoType = ModContent.NPCType<Flamingo>();      break;
                    case 2: flamingoType = ModContent.NPCType<FlamingoWhite>(); break;
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

