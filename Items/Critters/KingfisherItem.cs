using NaturalVariety.NPCs.Critters;
using Terraria;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class KingfisherItem : BaseCritterItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kingfisher");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<Kingfisher>();
        }

    }
}