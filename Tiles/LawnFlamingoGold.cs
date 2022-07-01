using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NaturalVariety.Items.Furniture;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace NaturalVariety.Tiles
{
    public class LawnFlamingoGold : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Origin = new Point16(0, 2);
            TileObjectData.newTile.RandomStyleRange = 2;
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newTile.StyleWrapLimit = 2;
            TileObjectData.newTile.StyleMultiplier = 2;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(2);
            TileObjectData.addTile(Type);

            AddMapEntry(Color.Gold, Language.GetText("MapObject.LawnFlamingo"));
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 48, ModContent.ItemType<LawnFlamingoGoldItem>());
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (Main.gamePaused || !Main.instance.IsActive || Lighting.UpdateEveryFrame)
            {
                return;
            }

            Vector2 position = new(i * 16 + 2, j * 16 + 3);

            if (Main.rand.NextBool(20))
            {
                int goldDust = Dust.NewDust(position, 2, 3, Main.rand.Next(232, 234), 0, 0, 20);
                Main.dust[goldDust].velocity *= 0;
                Main.dust[goldDust].noGravity = true;
            }
        }

    }
}
