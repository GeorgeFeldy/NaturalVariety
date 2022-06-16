using NaturalVariety.Items.Critters;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;


namespace NaturalVariety.NPCs.Critters
{
    public class Pochard : BaseWaterfowl
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/Pochard";

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<PochardItem>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "The common pochard (Aythya ferina) is a medium-sized diving duck. The adult male has a long dark bill with a grey band, a red head and neck, a black breast, red eyes and a grey back. The adult female has a brown head and body and a narrower grey bill-band.")
            });
        }


        public override void AI()
        {
            base.AI();
            if (Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(SoundID.Duck);
            }
        }
    }
}

