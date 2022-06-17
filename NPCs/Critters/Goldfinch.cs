using NaturalVariety.Items.Critters;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
// using NaturalVariety.Items.Placeable.Banners;

namespace NaturalVariety.NPCs.Critters
{
    public class Goldfinch : BaseSongbird
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<GoldfinchItem>();
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                    "The European goldfinch or simply the Goldfinch (Carduelis carduelis) is a small passerine bird in the finch family Fringillidae. The breeding male has a red face " +
                    "with black markings around the eyes, and a black-and-white head. The back and flanks are buff or chestnut brown. " +
                    "The black wings have a broad yellow bar. The tail is black and the rump is white. Males and females are very similar, " +
                    "but females have a slightly smaller red area on the face.")
            });
        }


    }
}