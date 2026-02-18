using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items
{
	/// <summary>
	/// Pesto Platino - Versión mejorada del revólver Pesto.
	/// Tiene outlines platinados, auto-fire y sus balas infligen fuego leve.
	/// Se craftea con Pesto + barras de platino en un yunque.
	/// </summary>
	public class PestoPlatino : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 24;
			Item.damage = 22;
			Item.DamageType = DamageClass.Ranged;
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4f;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.PestoPlatinoBullet>();
			Item.shootSpeed = 10f;
			Item.useAmmo = ModContent.ItemType<BalaPesto>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<Pesto>())
				.AddIngredient(ItemID.PlatinumBar, 10)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
