using NaturalVariety.NPCs.Critters;
using Terraria;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class BeeEaterItem : BaseCritterItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bee-eater");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<BeeEater>();
        }
    }
}