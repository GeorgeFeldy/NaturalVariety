using NaturalVariety.NPCs.Critters;
using Terraria;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class EurasianJayItem : BaseCritterItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jay");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<EurasianJay>();
        }
    }
}

