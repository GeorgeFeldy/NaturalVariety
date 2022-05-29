using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using NaturalVariety.NPCs.Critters;

namespace NaturalVariety.Items.Critters
{
    public class GoldDuckItem : CritterItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Duck");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<GoldDuck>();
            Item.value = Item.sellPrice(gold: 10);
        }

        public override void AddRecipes()
        {
            Recipe delightRecipe = Mod.CreateRecipe(ItemID.GoldenDelight);
            delightRecipe.AddIngredient<GoldDuckItem>();
            delightRecipe.AddTile(TileID.CookingPots);
            delightRecipe.Register();
        }
    }
}