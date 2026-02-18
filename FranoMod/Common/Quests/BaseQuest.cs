using Microsoft.Xna.Framework;
using Terraria;

namespace FranoMod.Common.Quests
{
	/// <summary>
	/// Clase base abstracta para todos los quests de Frano.
	/// Para agregar un nuevo quest, crear una clase que herede de BaseQuest
	/// e implementar los métodos abstractos. Luego registrarla en QuestManager.
	/// </summary>
	public abstract class BaseQuest
	{
		public abstract string Name { get; }
		public abstract string Description { get; }
		public abstract string CompletionMessage { get; }

		/// <summary>
		/// El tipo de item que el jugador debe entregar para completar el quest.
		/// </summary>
		public abstract int RequiredItemType { get; }

		/// <summary>
		/// Profundidad máxima en tiles donde se puede colocar el cofre del quest.
		/// Usar Main.worldSurface para superficie, Main.rockLayer para pre-cavernas, etc.
		/// Retornar int.MaxValue para sin límite.
		/// </summary>
		public abstract double MaxChestDepth { get; }

		/// <summary>
		/// Se ejecuta al activar el quest. Coloca el item requerido en un cofre aleatorio
		/// que cumpla con los criterios de profundidad.
		/// </summary>
		/// <returns>Posición del cofre en coordenadas del mundo, o Vector2.Zero si falla.</returns>
		public virtual Vector2 Activate()
		{
			int itemType = RequiredItemType;
			double maxDepth = MaxChestDepth;

			// Recopilar cofres elegibles
			var eligibleChests = new System.Collections.Generic.List<int>();
			for (int i = 0; i < Main.maxChests; i++)
			{
				Chest chest = Main.chest[i];
				if (chest == null)
					continue;

				if (chest.y > maxDepth)
					continue;

				// Verificar que el cofre tenga al menos un slot vacío
				bool hasSpace = false;
				for (int j = 0; j < Chest.maxItems; j++)
				{
					if (chest.item[j] == null || chest.item[j].IsAir)
					{
						hasSpace = true;
						break;
					}
				}

				if (hasSpace)
					eligibleChests.Add(i);
			}

			if (eligibleChests.Count == 0)
				return Vector2.Zero;

			// Seleccionar cofre aleatorio
			int chosenIndex = eligibleChests[Main.rand.Next(eligibleChests.Count)];
			Chest chosenChest = Main.chest[chosenIndex];

			// Colocar el item en el primer slot vacío
			for (int j = 0; j < Chest.maxItems; j++)
			{
				if (chosenChest.item[j] == null || chosenChest.item[j].IsAir)
				{
					chosenChest.item[j] = new Item();
					chosenChest.item[j].SetDefaults(itemType);
					break;
				}
			}

			return new Vector2(chosenChest.x * 16f, chosenChest.y * 16f);
		}

		/// <summary>
		/// Verifica si el jugador tiene el item requerido en su inventario.
		/// </summary>
		public virtual bool CanComplete(Player player)
		{
			return player.HasItem(RequiredItemType);
		}

		/// <summary>
		/// Consume el item requerido del inventario del jugador.
		/// </summary>
		public virtual void ConsumeRequiredItem(Player player)
		{
			for (int i = 0; i < player.inventory.Length; i++)
			{
				if (player.inventory[i].type == RequiredItemType)
				{
					player.inventory[i].TurnToAir();
					return;
				}
			}
		}

		/// <summary>
		/// Se ejecuta al completar el quest. Override para dar recompensas específicas.
		/// </summary>
		public abstract void OnComplete(Player player, NPC frano);
	}
}
