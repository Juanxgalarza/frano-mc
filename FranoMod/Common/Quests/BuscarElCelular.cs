using Terraria;
using Terraria.ModLoader;
using FranoMod.Content.Items;
using FranoMod.Content.Items.Gorros;

namespace FranoMod.Common.Quests
{
	/// <summary>
	/// Quest 3: "Buscar el celular"
	/// Frano perdió su celular nuevo en un cofre random.
	/// Al entregarlo, te regala "El Gorro", un beanie con buena defensa para el inicio.
	/// </summary>
	public class BuscarElCelular : BaseQuest
	{
		public override string Name => "Buscar el celular";

		public override string Description =>
			"¡Loco, perdí el celular nuevo! Se me debe haber caído en algún cofre por ahí... " +
			"Si me lo encontrás y me lo traés, te regalo algo que te va a servir. ¡Mirá el mapa!";

		public override string CompletionMessage =>
			"¡Eeeh, mi celular! ¡Sos un genio, loco! " +
			"Tomá, te regalo El Gorro. Es un beanie que me protegió mucho al principio. " +
			"Si querés, lo podés mejorar con minerales en un yunque.";

		public override int RequiredItemType => ModContent.ItemType<CelularDeFrano>();

		// Cualquier profundidad
		public override double MaxChestDepth => double.MaxValue;

		public override void OnComplete(Player player, NPC frano)
		{
			// Entregar El Gorro al jugador
			player.QuickSpawnItem(frano.GetSource_GiftOrReward(), ModContent.ItemType<ElGorro>());
		}
	}
}
