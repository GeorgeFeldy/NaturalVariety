using NaturalVariety.Items.Critters;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{
    public class FrogBlue : BaseFrog
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Poison Dart Frog");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<FrogBlueItem>();
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,

                new FlavorTextBestiaryInfoElement(
                    "The blue poison dart frog is a variant of the poison dart frog (Dendrobates tinctorius) found in the forests surrounded by the Sipaliwini Savanna, which is located in southern Suriname and adjacent far northern Brazil.")
            });
        }

    }
}