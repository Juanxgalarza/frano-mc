using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items
{
	public class BalaPesto : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 8;
			Item.height = 8;
			Item.damage = 7;
			Item.DamageType = DamageClass.Ranged;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 1f;
			Item.value = Item.buyPrice(copper: 5);
			Item.rare = ItemRarityID.White;
			Item.shoot = ModContent.ProjectileType<Projectiles.PestoProjectile>();
			Item.shootSpeed = 4f;
			Item.ammo = Item.type;
		}

		public override void AddRecipes()
		{
			CreateRecipe(50)
				.AddIngredient(ItemID.MusketBall, 50)
				.AddIngredient(ItemID.GreenDye)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
