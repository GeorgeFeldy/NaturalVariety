using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NaturalVariety.Mechanics
{
    public class NaturalVarietyRecipeGroups : ModSystem
    {
        public override void AddRecipeGroups()
        {
            int index;

            // add mod birds to recipe group
            if (RecipeGroup.recipeGroupIDs.ContainsKey("Birds"))
            {
                index = RecipeGroup.recipeGroupIDs["Birds"];
                RecipeGroup group = RecipeGroup.recipeGroups[index];
                group.ValidItems.Add(ModContent.ItemType<Items.Critters.BlackbirdItem>());
                group.ValidItems.Add(ModContent.ItemType<Items.Critters.EurasianJayItem>());
                group.ValidItems.Add(ModContent.ItemType<Items.Critters.GoldfinchItem>());

            }

            // add mod ducks to recipe group
            if (RecipeGroup.recipeGroupIDs.ContainsKey("Ducks"))
            {
                index = RecipeGroup.recipeGroupIDs["Ducks"];
                RecipeGroup group = RecipeGroup.recipeGroups[index];
                group.ValidItems.Add(ModContent.ItemType<Items.Critters.PochardItem>());
                group.ValidItems.Add(ModContent.ItemType<Items.Critters.FerruginousDuckItem>());

            }
        }
    }

}