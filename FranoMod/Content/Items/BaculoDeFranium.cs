using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items
{
	/// <summary>
	/// Báculo de Franium - Vara mágica potente.
	/// Dispara un orbe gelatinoso grande de Franium.
	/// Más daño que la espada pero con costo de maná. Pre-hardmode avanzado.
	/// </summary>
	public class BaculoDeFranium : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.staff[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.damage = 34;
			Item.DamageType = DamageClass.Magic;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 5f;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item43;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.mana = 10;
			Item.shoot = ModContent.ProjectileType<Projectiles.FraniumOrbLarge>();
			Item.shootSpeed = 7f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<FraniumOre>(), 22)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
