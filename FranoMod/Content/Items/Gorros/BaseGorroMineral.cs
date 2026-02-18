using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items.Gorros
{
	/// <summary>
	/// Clase base abstracta para todas las variantes minerales de El Gorro.
	/// Cada variante define su mineral, defensa, rareza y valor.
	/// Las recetas de mejora (anvil) y reversión (workbench) se generan automáticamente.
	/// </summary>
	public abstract class BaseGorroMineral : ModItem
	{
		protected abstract int GorroDefense { get; }
		protected abstract int BarItemID { get; }
		protected abstract int BarCount { get; }
		protected abstract int GorroRarity { get; }
		protected abstract int GorroValue { get; }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.defense = GorroDefense;
			Item.value = GorroValue;
			Item.rare = GorroRarity;
		}

		public override void AddRecipes()
		{
			// Mejora: El Gorro + 5 barras en Anvil → Gorrito de mineral
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<ElGorro>())
				.AddIngredient(BarItemID, BarCount)
				.AddTile(TileID.Anvils)
				.Register();

			// Reversión: Gorrito de mineral en WorkBench → El Gorro + barras devueltas
			Recipe.Create(ModContent.ItemType<ElGorro>())
				.AddIngredient(Type)
				.AddTile(TileID.WorkBenches)
				.AddOnCraftCallback((recipe, item, consumedItems, destination) =>
				{
					Main.LocalPlayer.QuickSpawnItem(
						Main.LocalPlayer.GetSource_Misc("GorroDeconstruct"),
						BarItemID, BarCount);
				})
				.Register();
		}
	}
}
