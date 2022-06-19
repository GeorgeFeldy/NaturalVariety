using NaturalVariety.NPCs.Critters;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class FrogYellowItem : BaseCritterItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Citronella Poison Dart Frog");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<FrogYellow>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine dartFrogTooltip = new TooltipLine(Mod, "PoisonousFrogTooltip", "You shouldn't eat this!");
            tooltips.Add(dartFrogTooltip);
        }

    }
}