using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using NaturalVariety.NPCs.Critters;

namespace NaturalVariety.Items.Critters
{
    public class GoldSeagullItem : CritterItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Seagull");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<GoldSeagull>();
            Item.value = Item.sellPrice(gold: 10);
        }

        public override void AddRecipes()
        {
            Recipe delightRecipe = Mod.CreateRecipe(ItemID.GoldenDelight);
            delightRecipe.AddIngredient<GoldSeagullItem>();
            delightRecipe.AddTile(TileID.CookingPots);
            delightRecipe.Register();
        }
    }
}