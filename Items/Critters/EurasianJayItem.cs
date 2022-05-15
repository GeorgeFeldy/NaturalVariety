using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using NaturalVariety.NPCs.Critters;

namespace NaturalVariety.Items.Critters
{
    public class EurasianJayItem : CritterItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eurasian Jay");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<EurasianJay>();
        }
    }
}

