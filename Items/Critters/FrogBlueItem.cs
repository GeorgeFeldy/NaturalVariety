using NaturalVariety.NPCs.Critters;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class FrogBlueItem : BaseCritterItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Poison Dart Frog");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<FrogBlue>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine blueFrogTooltip = new TooltipLine(Mod, "PoisonousFrogTooltip", "You shouldn't eat this!");
            tooltips.Add(blueFrogTooltip);
        }

    }
}