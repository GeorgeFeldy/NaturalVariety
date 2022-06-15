using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using NaturalVariety.NPCs.Critters;

namespace NaturalVariety.Items.Critters
{
    public class BlueFrogItem : CritterItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Frog");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<BlueFrog>();
        }

        public override void AddRecipes()
        {
            Recipe roastedRecipe = Mod.CreateRecipe(ItemID.FrogStatue);
            roastedRecipe.AddIngredient<BlueFrogItem>(5);
            roastedRecipe.AddIngredient(ItemID.StoneBlock, 50);
            roastedRecipe.AddTile(TileID.HeavyWorkBench);
            roastedRecipe.AddCondition(Recipe.Condition.InGraveyardBiome);
            roastedRecipe.Register();
        }
    }
}