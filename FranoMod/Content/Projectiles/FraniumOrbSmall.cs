using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Projectiles
{
	/// <summary>
	/// Orbe gelatinoso pequeño de Franium (proyectil de la espada).
	/// Se mueve en arco con gravedad leve, rota y pulsa como gel.
	/// </summary>
	public class FraniumOrbSmall : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 2;
			Projectile.timeLeft = 300;
			Projectile.light = 0.5f;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.alpha = 40;
		}

		public override void AI()
		{
			// Gravedad leve (arco suave)
			Projectile.velocity.Y += 0.06f;

			// Rotación
			Projectile.rotation += 0.15f;

			// Efecto gelatinoso (pulso de escala)
			Projectile.scale = 1f + (float)Math.Sin(Projectile.ai[0] * 0.12f) * 0.08f;
			Projectile.ai[0]++;

			// Trail de partículas púrpura
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
					DustID.PurpleTorch, 0f, 0f, 100, default, 0.7f);
				dust.noGravity = true;
				dust.velocity *= 0.3f;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(120, 80, 200, 150);
		}

		public override void OnKill(int timeLeft)
		{
			for (int i = 0; i < 8; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
					DustID.PurpleTorch, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f),
					80, default, 1f);
				dust.noGravity = true;
			}
		}
	}
}
