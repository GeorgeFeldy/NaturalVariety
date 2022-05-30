using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using NaturalVariety.Projectiles.Minions;

namespace NaturalVariety.Buffs.Minions
{
    public class RaptorMinionBuff : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raptor minion");
			Description.SetDefault("A falcon will fight for you during daytime, an owl during nighttime");

			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex)
		{
			// If the minions exist reset the buff time, otherwise remove the buff from the player
			if (player.ownedProjectileCounts[ModContent.ProjectileType<RaptorMinion>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
