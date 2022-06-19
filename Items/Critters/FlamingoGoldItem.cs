using NaturalVariety.NPCs.Critters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class FlamingoGoldItem : BaseCritterItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Gold Flamingo");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<FlamingoGold>();
            Item.value = Item.sellPrice(gold: 20);
        }

        public override void AddRecipes()
        {
            Recipe delightRecipe = Mod.CreateRecipe(ItemID.GoldenDelight, 2);
            delightRecipe.AddIngredient<FlamingoGoldItem>();
            delightRecipe.AddTile(TileID.CookingPots);
            delightRecipe.Register();
        }
    }
}