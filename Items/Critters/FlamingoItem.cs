using Terraria;
using Terraria.ModLoader;
using NaturalVariety.NPCs.Critters;

namespace NaturalVariety.Items.Critters
{
    public class FlamingoItem : CritterItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flamingo");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<Flamingo>();
            Item.value = Item.sellPrice(silver: 10);
        }
    }
}