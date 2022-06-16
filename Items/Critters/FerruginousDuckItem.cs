using NaturalVariety.NPCs.Critters;
using Terraria;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class FerruginousDuckItem : BaseCritterItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ferruginous Duck");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<FerruginousDuck>();
            Item.value = Item.sellPrice(silver: 7, copper: 50);
        }

    }
}