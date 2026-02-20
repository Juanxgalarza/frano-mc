using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Projectiles
{
	/// <summary>
	/// Orbe gelatinoso grande de Franium (proyectil del báculo).
	/// Más grande y potente que el orbe pequeño. Se mueve más lento pero
	/// penetra más enemigos y genera más partículas.
	/// </summary>
	public class FraniumOrbLarge : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 420;
			Projectile.light = 0.7f;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.alpha = 30;
		}

		public override void AI()
		{
			// Gravedad más leve (flota más)
			Projectile.velocity.Y += 0.04f;

			// Rotación
			Projectile.rotation += 0.2f;

			// Efecto gelatinoso más pronunciado
			Projectile.scale = 1f + (float)Math.Sin(Projectile.ai[0] * 0.1f) * 0.12f;
			Projectile.ai[0]++;

			// Trail de partículas más denso
			if (Main.rand.NextBool(2))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
					DustID.PurpleTorch, 0f, 0f, 80, default, 1.0f);
				dust.noGravity = true;
				dust.velocity *= 0.4f;
			}

			// Partículas extra de brillo
			if (Main.rand.NextBool(5))
			{
				Dust shine = Dust.NewDustDirect(Projectile.Center, 1, 1,
					DustID.PurpleCrystalShard, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f),
					0, default, 0.6f);
				shine.noGravity = true;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(100, 60, 180, 120);
		}

		public override void OnKill(int timeLeft)
		{
			// Explosión grande de partículas
			for (int i = 0; i < 15; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
					DustID.PurpleTorch, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f),
					60, default, 1.2f);
				dust.noGravity = true;
			}

			for (int i = 0; i < 4; i++)
			{
				Dust flame = Dust.NewDustDirect(Projectile.Center, 4, 4,
					DustID.PurpleCrystalShard, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f),
					0, default, 0.9f);
				flame.noGravity = true;
			}
		}
	}
}
