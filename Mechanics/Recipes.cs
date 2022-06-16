using NaturalVariety.Items.Critters;
using NaturalVariety.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


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

                foreach (int item in NPCTypesHelper.SongbirdItems)
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

            RecipeGroup frogRecipeGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.Frog)}",
                ItemID.Frog,
                ModContent.ItemType<FrogBlueItem>()
                );

            RecipeGroup dartFrogRecipeGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Dart Frog",
                ModContent.ItemType<FrogBlueItem>()
                );

            frogRecipeGroupIndex = RecipeGroup.RegisterGroup("Frog", frogRecipeGroup);
            dartFrogRecipeGroupIndex = RecipeGroup.RegisterGroup("DartFrog", dartFrogRecipeGroup);

        }

        public override void AddRecipes()
        {
            Recipe poisonKnifeRecipe = Mod.CreateRecipe(ItemID.PoisonedKnife, 250);
            poisonKnifeRecipe.AddRecipeGroup(dartFrogRecipeGroupIndex);
            poisonKnifeRecipe.AddIngredient(ItemID.ThrowingKnife, 250);
            poisonKnifeRecipe.Register();


            foreach (int item in NPCTypesHelper.SongbirdItems)
            {
                Recipe roastedRecipe = Mod.CreateRecipe(ItemID.RoastedBird);
                roastedRecipe.AddIngredient(item);
                roastedRecipe.AddTile(TileID.CookingPots);
                roastedRecipe.Register();
            }

            foreach (int item in NPCTypesHelper.NotIntersect(NPCTypesHelper.DuckItems, NPCTypesHelper.GoldenCritterItems))
            {
                Recipe roastedRecipe = Mod.CreateRecipe(ItemID.RoastedDuck);
                roastedRecipe.AddIngredient(item);
                roastedRecipe.AddTile(TileID.CookingPots);
                roastedRecipe.Register();
            }

            foreach (int item in NPCTypesHelper.NotIntersect(NPCTypesHelper.GoldenCritterItems, ModContent.ItemType<FlamingoGoldItem>()))
            {
                Recipe delightRecipe = Mod.CreateRecipe(ItemID.GoldenDelight);
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