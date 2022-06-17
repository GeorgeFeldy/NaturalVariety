using NaturalVariety.Items.Critters;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.Critters
{
    public class FrogRed : BaseFrog
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Poison Dart Frog");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<FrogRedItem>();
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,

                new FlavorTextBestiaryInfoElement(
                    "The strawberry poison dart frog (Oophaga pumilio) is a species of small, bright red and blue poison dart frog found in tropical rainforests. " +
                    "It's diet causes the skin of the amphibian to become toxic in nature when certain subspecies of mites and ants are ingested.")
            });
        }

    }
}