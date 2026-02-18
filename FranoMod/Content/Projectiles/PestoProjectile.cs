using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Projectiles
{
	public class PestoProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.extraUpdates = 1;
			Projectile.light = 0.3f;
			AIType = ProjectileID.Bullet;
		}

		public override void OnKill(int timeLeft)
		{
			// Partículas al impactar
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
					DustID.Smoke, 0f, 0f, 100, default, 0.8f);
			}
		}
	}
}
