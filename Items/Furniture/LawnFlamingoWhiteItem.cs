using NaturalVariety.NPCs.Critters;
using NaturalVariety.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Furniture
{
    public class LawnFlamingoWhiteItem : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lawn Flamingo");
        }
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<LawnFlamingoWhite>());
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 10);
            Item.maxStack = 99;
            Item.width = 20;
            Item.height = 20;
        }
    }
}