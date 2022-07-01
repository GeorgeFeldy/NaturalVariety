using NaturalVariety.NPCs.Critters;
using NaturalVariety.Tiles;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Furniture
{
    public class LawnFlamingoGoldItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lawn Flamingo");
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<LawnFlamingoGold>());
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(gold: 20);
            Item.maxStack = 99;
            Item.width = 20;
            Item.height = 20;
        }
    }
}