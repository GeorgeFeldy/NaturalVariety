using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using NaturalVariety.NPCs.Critters;

namespace NaturalVariety.Items.Critters
{
    public class FerruginousDuckItem : CritterItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ferruginous Duck");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<FerruginousDuck>();
            Item.value = Item.sellPrice(silver: 7, copper: 50);
        }

        public override void AddRecipes()
        {
            Recipe roastedRecipe = Mod.CreateRecipe(ItemID.RoastedDuck);
            roastedRecipe.AddIngredient<FerruginousDuckItem>();
            roastedRecipe.AddTile(TileID.CookingPots);
            roastedRecipe.Register();
        }
    }
}