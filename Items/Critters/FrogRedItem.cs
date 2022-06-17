using NaturalVariety.NPCs.Critters;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class FrogRedItem : BaseCritterItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strawberry Poison Dart Frog");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<FrogRed>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine dartFrogTooltip = new TooltipLine(Mod, "RedPoisonousFrogTooltip", "Not strawberry flavoured!");
            tooltips.Add(dartFrogTooltip);
        }

    }
}