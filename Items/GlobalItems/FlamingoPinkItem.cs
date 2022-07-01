using NaturalVariety.NPCs.Critters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{
    public class LawnFlamingoGlobalItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.LawnFlamingo;

        public override void SetDefaults(Item item)
        {
            item.value = Item.sellPrice(silver: 10);
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.LawnFlamingo);
            recipe.AddIngredient<FlamingoPinkItem>();
            recipe.AddTile(TileID.Solidifier);
            recipe.Register();
        }
    }
}