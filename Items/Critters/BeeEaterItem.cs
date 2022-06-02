using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using NaturalVariety.NPCs.Critters;

namespace NaturalVariety.Items.Critters
{
    public class BeeEaterItem : CritterItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BeeEater");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<BeeEater>();
        }

        public override void AddRecipes()
        {
            Recipe roastedRecipe = Mod.CreateRecipe(ItemID.RoastedBird);
            roastedRecipe.AddIngredient<BeeEaterItem>();
            roastedRecipe.AddTile(TileID.CookingPots);
            roastedRecipe.Register();
        }
    }
}