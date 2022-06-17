using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Ammo
{
    public class PoisonArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Poison Arrow");
			Tooltip.SetDefault("Inflicts target with poison");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 10;
			Item.height = 28;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 2.5f;
			Item.value = Item.sellPrice(copper: 3);
			Item.rare = ItemRarityID.Blue;
			Item.shoot = ModContent.ProjectileType<PoisonArrow_Proj>();
			Item.shootSpeed = 3.4f;
			Item.ammo = AmmoID.Arrow;
		}
	}
}
