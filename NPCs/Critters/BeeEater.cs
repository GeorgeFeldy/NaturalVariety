using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

using NaturalVariety.Items.Critters;

namespace NaturalVariety.NPCs.Critters
{ 
    public class BeeEater: Songbird 
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<BeeEaterItem>();
        }
     
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                    "The European bee-eater (Merops apiaster) is a near passerine bird in the bee-eater family, Meropidae. It breeds in southern Europe and in parts of north Africa and western Asia. This species, like other bee-eaters, is a richly coloured, slender bird. It has brown and yellow upper parts, whilst the wings and under parts are blueish-green."
                    )
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return SpawnCondition.SurfaceJungle.Chance + SpawnCondition.TownJungleCritter.Chance; // Spawn with regular bird chance. 

        }




    }
}