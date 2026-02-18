using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Projectiles
{
	/// <summary>
	/// Proyectil del Pesto Platino. Similar al PestoProjectile pero
	/// inflige debuff OnFire (fuego leve) al impactar enemigos.
	/// </summary>
	public class PestoPlatinoBullet : ModProjectile
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
			Projectile.light = 0.4f;
			AIType = ProjectileID.Bullet;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			// Fuego leve: 3 segundos de OnFire
			target.AddBuff(BuffID.OnFire, 180);
		}

		public override void OnKill(int timeLeft)
		{
			// Partículas de fuego y humo al impactar
			for (int i = 0; i < 4; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
					DustID.Smoke, 0f, 0f, 100, default, 0.8f);
			}
			for (int i = 0; i < 3; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
					DustID.Torch, 0f, 0f, 0, default, 1f);
			}
		}
	}
}
