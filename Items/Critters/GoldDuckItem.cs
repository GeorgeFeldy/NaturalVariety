using NaturalVariety.NPCs.Critters;
using Terraria;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class GoldDuckItem : BaseCritterItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Duck");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<GoldDuck>();
            Item.value = Item.sellPrice(gold: 10);
        }

    }
}