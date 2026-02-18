using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FranoMod.Content.Items;

namespace FranoMod.Common.Quests
{
	/// <summary>
	/// Quest 2: "Traer la ropa"
	/// Frano marca un cofre aleatorio no tan profundo.
	/// El cofre contiene "El Traje".
	/// Al entregarlo, Frano se viste de smoking,
	/// entrega "Pesto" (revólver) y empieza a vender balas.
	/// </summary>
	public class TraerLaRopa : BaseQuest
	{
		public override string Name => "Traer la ropa";

		public override string Description =>
			"Mirá, no puedo andar en shorts para siempre... Hay un cofre no muy profundo " +
			"que tiene mi traje. Si me lo traés, te voy a dar algo especial. ¡Revisá el mapa!";

		public override string CompletionMessage =>
			"¡Por fin! Ahora sí parezco una persona decente. " +
			"Tomá, te regalo a Pesto, mi revólver de confianza. " +
			"Y de ahora en más te vendo las balas también.";

		public override int RequiredItemType => ModContent.ItemType<ElTraje>();

		// Solo cofres no tan profundos (por encima de la capa de roca)
		public override double MaxChestDepth => Main.rockLayer;

		public override void OnComplete(Player player, NPC frano)
		{
			// Entregar Pesto al jugador
			player.QuickSpawnItem(frano.GetSource_GiftOrReward(), ModContent.ItemType<Pesto>());

			// Marcar en el world system que Frano tiene traje
			var worldSystem = ModContent.GetInstance<Systems.FranoWorldSystem>();
			worldSystem.FranoHasSuit = true;
		}
	}
}
