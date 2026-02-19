using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FranoMod.Content.Items;
using FranoMod.Content.Tiles;

namespace FranoMod.Common.Quests
{
	/// <summary>
	/// Quest 4: "Minar el Franium"
	/// Después de completar el quest del gorro, Frano revela la existencia del Franium.
	/// Genera una veta pequeña en las cavernas y la marca en el mapa.
	/// Al completar, genera muchas vetas en todo el mundo subterráneo.
	/// </summary>
	public class MinarFranium : BaseQuest
	{
		public override string Name => "Minar el Franium";

		public override string Description =>
			"Escuchá, descubrí un mineral re raro que le dicen Franium. " +
			"Hay una veta en las cavernas que marqué en el mapa. " +
			"Traeme un pedazo y te prometo que va a valer la pena. ¡Mirá el mapa!";

		public override string CompletionMessage =>
			"¡Mirá esto! ¡El Franium es real! Ahora que confirmé que existe, " +
			"voy a hacer que aparezcan más vetas por todo el subterráneo. " +
			"Este mineral va a cambiar todo, ya vas a ver...";

		public override int RequiredItemType => ModContent.ItemType<FraniumOre>();

		// No se usa para este quest (no es chest-based)
		public override double MaxChestDepth => double.MaxValue;

		/// <summary>
		/// En vez de colocar un item en un cofre, genera una veta de Franium
		/// en las cavernas y retorna su posición para el marcador del mapa.
		/// </summary>
		public override Vector2 Activate()
		{
			int tileType = ModContent.TileType<FraniumOreTile>();

			for (int attempts = 0; attempts < 100; attempts++)
			{
				int x = Main.rand.Next(100, Main.maxTilesX - 100);
				int y = Main.rand.Next((int)Main.rockLayer + 50, Main.maxTilesY - 200);

				if (Main.tile[x, y].HasTile && Main.tile[x, y].TileType == TileID.Stone)
				{
					// Generar una veta pequeña pero visible
					WorldGen.TileRunner(x, y, Main.rand.Next(5, 9), Main.rand.Next(5, 9), tileType);
					return new Vector2(x * 16f, y * 16f);
				}
			}

			return Vector2.Zero;
		}

		public override void OnComplete(Player player, NPC frano)
		{
			int tileType = ModContent.TileType<FraniumOreTile>();

			// Generar muchas vetas de Franium por todas las cavernas del mundo
			for (int i = 0; i < 200; i++)
			{
				int x = Main.rand.Next(100, Main.maxTilesX - 100);
				int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 200);

				WorldGen.TileRunner(x, y, Main.rand.Next(3, 7), Main.rand.Next(3, 7), tileType);
			}

			player.QuickSpawnItem(frano.GetSource_GiftOrReward(), ItemID.GoldCoin, 5);
		}
	}
}
