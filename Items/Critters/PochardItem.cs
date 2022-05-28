using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using NaturalVariety.NPCs.Critters;

namespace NaturalVariety.Items.Critters
{
    public class PochardItem : CritterItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pochard");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<Pochard>();
            Item.value = Item.sellPrice(silver: 7, copper: 50);
        }

        public override void AddRecipes()
        {
            Recipe roastedRecipe = Mod.CreateRecipe(ItemID.RoastedDuck);
            roastedRecipe.AddIngredient<PochardItem>();
            roastedRecipe.AddTile(TileID.CookingPots);
            roastedRecipe.Register();
        }
    }
}