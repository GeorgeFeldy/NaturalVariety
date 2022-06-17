using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.Items.Ammo
{
    public class PoisonArrow_Proj : ModProjectile
	{
        public override string Texture => "NaturalVariety/Items/Ammo/PoisonArrow";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Arrow");
		}
		public override void SetDefaults()
		{
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.timeLeft = 1200;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = true;
			AIType = ProjectileID.WoodenArrowFriendly;
		}

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            Projectile.velocity.Y += 0.03f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Main.rand.NextBool()) // 50% chance to inflict poison 
                {
                    target.AddBuff(BuffID.Poisoned, 360);
                    Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Poisoned, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                    target.netUpdate = true;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, oldVelocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }

        public override void Kill(int timeLeft)
        {

            if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Main.rand.NextBool(4))
                {
                    int idx = Item.NewItem(Entity.GetSource_DropAsItem(), Projectile.position, (int)Projectile.width, (int)Projectile.height, ModContent.ItemType<PoisonArrow>());
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, idx, 1f);
                }
            }
        }
    }
}
