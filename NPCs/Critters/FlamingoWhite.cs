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
    public class FlamingoWhite : WadingBird 
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/FlamingoWhite";

        // public ref float AI_spawnInFlocks => ref NPC.ai[3];

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Flamingo");
        }

        public override void SetDefaults()
        {
            base.SetDefaults(); 
            NPC.catchItem = (short)ModContent.ItemType<FlamingoWhiteItem>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f; // can only spawn via Flamingo spawner
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "The greater flamingo (Phoenicopterus roseus) is the most widespread and largest species of the flamingo family." + 
                   " It is found in Africa, the Indian subcontinent, the Middle East, and in southern Europe." + 
                   " Most of its plumage is white, but the primary and secondary flight feathers are black.")
            });
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

