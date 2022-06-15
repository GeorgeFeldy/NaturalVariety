using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using NaturalVariety.Items.Critters;

namespace NaturalVariety.NPCs.Critters
{
    public class BlueFrog : Frog 
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            //NPC.catchItem = (short)ModContent.ItemType<BlackbirdItem>();
            NPC.catchItem = ItemID.Frog;
        }
     
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                    "The blue poison dart frog is a variant of the poison dart frog (Dendrobates tinctorius) found in the forests surrounded by the Sipaliwini Savanna, which is located in southern Suriname and adjacent far northern Brazil.")
            });
        }

      
    }
}