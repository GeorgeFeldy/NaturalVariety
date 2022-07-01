using NaturalVariety.NPCs.Critters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class FlamingoPinkItem : BaseCritterItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Flamingo");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<FlamingoPink>();
            Item.value = Item.sellPrice(silver: 10);
        }

    }
}