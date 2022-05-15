using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace NaturalVariety.Items.Critters
{

    /// <summary>
    /// Generic class for all critter items in this mod 
    /// </summary>
    public abstract class CritterItem : ModItem
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
           Item.maxStack = 999;
           Item.consumable = true;
           Item.value = Item.buyPrice(silver: 1);
           Item.width = 12;
           Item.height = 12;            
           Item.noUseGraphic = true;      
        }

    }
}