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
                    "The blue poison dart frog (Dendrobates tinctorius \"azureus\") is a spotted bright blue morph of the poison dart frog found in equatorial forests. Like most species of the genus Dendrobates, D. tinctorius is highly toxic if consumed.")
            });
        }

    }
}