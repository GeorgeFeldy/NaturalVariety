using NaturalVariety.Items.Critters;
using NaturalVariety.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using NaturalVariety.Items;
using NaturalVariety.Items.Ammo;


namespace NaturalVariety.Mechanics
{
    public class Recipes : ModSystem
    {

        int frogRecipeGroupIndex;
        int dartFrogRecipeGroupIndex;

        public override void AddRecipeGroups()
        {
            int vanillaGroupIndex;

            // add mod birds to recipe group
            if (RecipeGroup.recipeGroupIDs.ContainsKey("Birds"))
            {
                vanillaGroupIndex = RecipeGroup.recipeGroupIDs["Birds"];
                RecipeGroup group = RecipeGroup.recipeGroups[vanillaGroupIndex];

                foreach (int item in ItemTypeCategorisation.SongbirdItems)
                {
                    group.ValidItems.Add(item);
                }
            }

            // add mod ducks to recipe group
            if (RecipeGroup.recipeGroupIDs.ContainsKey("Ducks"))
            {
                vanillaGroupIndex = RecipeGroup.recipeGroupIDs["Ducks"];
                RecipeGroup group = RecipeGroup.recipeGroups[vanillaGroupIndex];
                group.ValidItems.Add(ModContent.ItemType<PochardItem>());
                group.ValidItems.Add(ModContent.ItemType<FerruginousDuckItem>());

            }

            RecipeGroup frogRecipeGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.Frog)}", ItemID.Frog);

            foreach (int item in ItemTypeCategorisation.FrogItems)
            {
                frogRecipeGroup.ValidItems.Add(item);
            }

            RecipeGroup dartFrogRecipeGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Dart Frog", ModContent.ItemType<FrogBlueItem>()); // iconic is blue frog

            foreach (int item in ItemTypeCategorisation.NotIntersect(ItemTypeCategorisation.FrogItems, ModContent.ItemType<FrogBlueItem>())) // add the rest of the frog items to the dart frog recipe group
            {
                dartFrogRecipeGroup.ValidItems.Add(item);
            }

            frogRecipeGroupIndex = RecipeGroup.RegisterGroup("Frog", frogRecipeGroup);
            dartFrogRecipeGroupIndex = RecipeGroup.RegisterGroup("DartFrog", dartFrogRecipeGroup);

        }

        public override void AddRecipes()
        {
            Recipe poisonKnifeRecipe = Recipe.Create(ItemID.PoisonedKnife, 250);
            poisonKnifeRecipe.AddRecipeGroup(dartFrogRecipeGroupIndex);
            poisonKnifeRecipe.AddIngredient(ItemID.ThrowingKnife, 250);
            poisonKnifeRecipe.Register();

            Recipe poisonArrowRecipe = Recipe.Create(ModContent.ItemType<PoisonArrow>(), 250);
            poisonArrowRecipe.AddRecipeGroup(dartFrogRecipeGroupIndex);
            poisonArrowRecipe.AddIngredient(ItemID.WoodenArrow, 250);
            poisonArrowRecipe.Register();

            foreach (int item in ItemTypeCategorisation.SongbirdItems)
            {
                Recipe roastedRecipe = Recipe.Create(ItemID.RoastedBird);
                roastedRecipe.AddIngredient(item);
                roastedRecipe.AddTile(TileID.CookingPots);
                roastedRecipe.Register();
            }

            foreach (int item in ItemTypeCategorisation.NotIntersect(ItemTypeCategorisation.DuckItems, ItemTypeCategorisation.GoldenCritterItems))
            {
                Recipe roastedRecipe = Recipe.Create(ItemID.RoastedDuck);
                roastedRecipe.AddIngredient(item);
                roastedRecipe.AddTile(TileID.CookingPots);
                roastedRecipe.Register();
            }

            foreach (int item in ItemTypeCategorisation.NotIntersect(ItemTypeCategorisation.GoldenCritterItems, ModContent.ItemType<FlamingoGoldItem>()))
            {
                Recipe delightRecipe = Recipe.Create(ItemID.GoldenDelight);
                delightRecipe.AddIngredient(item);
                delightRecipe.AddTile(TileID.CookingPots);
                delightRecipe.Register();
            }
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ItemID.FrogStatue))
                {
                    recipe.RemoveIngredient(ItemID.Frog);
                    recipe.AddRecipeGroup(frogRecipeGroupIndex, 5);
                }
            }
        }
    }

}