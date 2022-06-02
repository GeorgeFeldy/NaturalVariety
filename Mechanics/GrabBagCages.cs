﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using NaturalVariety.Utils;
using System.Collections.Generic;

namespace NaturalVariety.Mechanics
{
    /// <summary>
    /// GlobalItem to edit each "cage" item found in Utils.CageHelper.cageList
    /// </summary>
    public class GrabBagCages : GlobalItem 
    {
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            return CageHelper.ItemIsCage(item);
        }

        public override bool CanRightClick(Item item)
        {
            return CageHelper.ItemIsCage(item);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
           TooltipLine cageIsGrabBagTooltip = new TooltipLine(Mod, "CageIsGrabBagTooltip", "Right click to release!");
           tooltips.Add(cageIsGrabBagTooltip);
        }

        public override void RightClick(Item item, Player player)
        {
            var entitySource = player.GetSource_OpenItem(item.type);
     
            foreach(Item recipeItem in CageHelper.recipeItemDict[item.type])
            {
                player.QuickSpawnItem(entitySource, recipeItem.type);
            }
        }
    }
}

