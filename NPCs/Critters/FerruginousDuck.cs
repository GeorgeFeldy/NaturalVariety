using NaturalVariety.Items.Critters;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;


namespace NaturalVariety.NPCs.Critters
{
    public class FerruginousDuck : BaseWaterfowl
    {


        public override string Texture => "NaturalVariety/NPCs/Critters/FerruginousDuck";

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<FerruginousDuckItem>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "The ferruginous duck, also ferruginous pochard, common white-eye or white-eyed pochard (Aythya nyroca) is a medium-sized diving duck from Eurosiberia. The breeding male is a rich, dark chestnut on the head, breast and flanks with contrasting pure white undertail coverts. In flight the white belly and underwing patch are visible. The females are duller and browner than the males. The male has a yellow eye and the females have a dark eye.")
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

