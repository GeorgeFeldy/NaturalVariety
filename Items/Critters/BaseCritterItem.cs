using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Critters
{

    /// <summary>
    /// Generic class for all critter items in this mod 
    /// </summary>
    public abstract class BaseCritterItem : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5; // research required count
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 5); // some will override this 
            Item.width = 12;
            Item.height = 12;
            Item.noUseGraphic = true;
        }

    }
}