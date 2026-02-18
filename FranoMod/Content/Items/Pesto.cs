using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items
{
	/// <summary>
	/// Pesto - Revólver básico verde gastado.
	/// Recompensa del quest "Traer la ropa".
	/// Usa BalaPesto como munición.
	/// </summary>
	public class Pesto : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 24;
			Item.damage = 14;
			Item.DamageType = DamageClass.Ranged;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3f;
			Item.value = Item.buyPrice(gold: 2);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = false;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.PestoProjectile>();
			Item.shootSpeed = 8f;
			Item.useAmmo = ModContent.ItemType<BalaPesto>();
		}
	}
}
