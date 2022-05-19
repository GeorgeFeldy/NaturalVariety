using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

using NaturalVariety.Items.Critters;
// using NaturalVariety.Items.Placeable.Banners;

namespace NaturalVariety.NPCs.Critters
{
    public class EurasianJay : Songbird 
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<EurasianJayItem>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                    "Eurasian Jay (Garrulus glandarius) is a species of passerine bird in the family Corvidae with boldly patterned " +
                    "plumage, typically having blue feathers in the wings or tail.")
            });
        }

      
    }
}