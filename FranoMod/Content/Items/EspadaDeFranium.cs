using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items
{
	/// <summary>
	/// Espada de Franium - Arma mágica con forma de espada.
	/// Dispara un orbe gelatinoso de Franium al atacar.
	/// Auto-cast, sin costo de maná. Pre-hardmode avanzado.
	/// </summary>
	public class EspadaDeFranium : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.damage = 24;
			Item.DamageType = DamageClass.Magic;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4f;
			Item.value = Item.buyPrice(gold: 3);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.mana = 0;
			Item.shoot = ModContent.ProjectileType<Projectiles.FraniumOrbSmall>();
			Item.shootSpeed = 9f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<FraniumOre>(), 15)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
