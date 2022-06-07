using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using NaturalVariety.NPCs.Critters;

namespace NaturalVariety.Items.Critters
{
    public class KingfisherItem : CritterItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kingfisher");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.makeNPC = (short)ModContent.NPCType<Kingfisher>();
        }

        public override void AddRecipes()
        {
            Recipe roastedRecipe = Mod.CreateRecipe(ItemID.RoastedBird);
            roastedRecipe.AddIngredient<KingfisherItem>();
            roastedRecipe.AddTile(TileID.CookingPots);
            roastedRecipe.Register();
        }
    }
}