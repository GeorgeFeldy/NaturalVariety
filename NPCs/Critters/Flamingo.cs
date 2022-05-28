using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.DataStructures;

using NaturalVariety.Items.Critters;
using NaturalVariety.Sounds;

namespace NaturalVariety.NPCs.Critters
{
    public class Flamingo : WadingBird 
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/Flamingo";

        // public ref float AI_spawnInFlocks => ref NPC.ai[3];

        public override void SetDefaults()
        {
            base.SetDefaults(); 
            NPC.catchItem = (short)ModContent.ItemType<FlamingoItem>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "The greater flamingo (Phoenicopterus roseus) is the most widespread and largest species of the flamingo family. It is found in Africa, the Indian subcontinent, the Middle East, and in southern Europe. Most of its plumage is pinkish-white, but the primary and secondary flight feathers are black.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float chance = SpawnCondition.OverworldWaterSurfaceCritter.Chance;
            return chance;
        }
        


        public override void OnSpawn(IEntitySource source)
        {

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            // small chance to recurse to NPC cap :D 
            bool spawnAnother = Main.rand.NextBool(3);

            if (spawnAnother)
            {
                int index = NPC.NewNPC(source, (int)NPC.position.X + Main.rand.Next(-30, NPC.width + 30), (int)NPC.position.Y + NPC.height,
                ModContent.NPCType<Flamingo>());

                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
            }
          
            base.OnSpawn(source);
        }

        public override void AI()
        {
            base.AI();
            if(Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(CustomSounds.FlamingoCall, NPC.position);    
            }
        }
    }

}

