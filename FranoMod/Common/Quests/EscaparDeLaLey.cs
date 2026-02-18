using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FranoMod.Content.Items;

namespace FranoMod.Common.Quests
{
	/// <summary>
	/// Quest 1: "Escapar de la ley"
	/// Frano marca un cofre aleatorio en el mundo.
	/// El cofre contiene "Orden de detención: Frano".
	/// Al entregarlo, Frano agrega más items a su tienda.
	/// </summary>
	public class EscaparDeLaLey : BaseQuest
	{
		public override string Name => "Escapar de la ley";

		public override string Description =>
			"Necesito que me hagas un favor urgente... Hay un cofre por ahí que tiene " +
			"una orden de detención con mi nombre. Si la encontrás y me la traés, " +
			"te voy a tener mejores cosas en la tienda. ¡Mirá el mapa!";

		public override string CompletionMessage =>
			"¡Genio! Con esto me saco a la ley de encima. " +
			"Ahora fijate mi tienda, agregué cosas nuevas para vos.";

		public override int RequiredItemType => ModContent.ItemType<OrdenDeDetencion>();

		// Cualquier cofre del mundo
		public override double MaxChestDepth => double.MaxValue;

		public override void OnComplete(Player player, NPC frano)
		{
			// Recompensa: algunas monedas
			player.QuickSpawnItem(frano.GetSource_GiftOrReward(), ItemID.GoldCoin, 3);
		}
	}
}
