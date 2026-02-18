using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.IO;

namespace FranoMod.Common.Quests
{
	/// <summary>
	/// Gestor modular de quests. Registra quests en orden y maneja la progresión.
	/// Para agregar un nuevo quest: crear clase que herede BaseQuest,
	/// y registrarla con RegisterQuest() en Initialize().
	/// </summary>
	public class QuestManager
	{
		private readonly List<BaseQuest> _quests = new();
		private readonly HashSet<int> _completedQuests = new();

		public int CurrentQuestIndex { get; set; } = -1;
		public Vector2 ActiveChestMarker { get; set; } = Vector2.Zero;
		public bool HasActiveQuest => CurrentQuestIndex >= 0 && CurrentQuestIndex < _quests.Count && !_completedQuests.Contains(CurrentQuestIndex);

		public BaseQuest CurrentQuest => CurrentQuestIndex >= 0 && CurrentQuestIndex < _quests.Count
			? _quests[CurrentQuestIndex]
			: null;

		public int QuestCount => _quests.Count;

		/// <summary>
		/// Registrar un quest nuevo. Se ejecutan en orden de registro.
		/// </summary>
		public void RegisterQuest(BaseQuest quest)
		{
			_quests.Add(quest);
		}

		/// <summary>
		/// Verificar si un quest específico fue completado.
		/// </summary>
		public bool IsQuestCompleted(int index)
		{
			return _completedQuests.Contains(index);
		}

		/// <summary>
		/// Verificar si todos los quests fueron completados.
		/// </summary>
		public bool AllQuestsCompleted()
		{
			return _completedQuests.Count >= _quests.Count;
		}

		/// <summary>
		/// Inicia el siguiente quest disponible.
		/// </summary>
		public bool StartNextQuest()
		{
			int nextIndex = -1;
			for (int i = 0; i < _quests.Count; i++)
			{
				if (!_completedQuests.Contains(i))
				{
					nextIndex = i;
					break;
				}
			}

			if (nextIndex < 0)
				return false;

			CurrentQuestIndex = nextIndex;
			Vector2 chestPos = _quests[nextIndex].Activate();
			ActiveChestMarker = chestPos;

			return chestPos != Vector2.Zero;
		}

		/// <summary>
		/// Intenta completar el quest actual si el jugador tiene el item.
		/// </summary>
		public bool TryCompleteCurrentQuest(Player player, NPC frano)
		{
			if (!HasActiveQuest)
				return false;

			BaseQuest quest = _quests[CurrentQuestIndex];
			if (!quest.CanComplete(player))
				return false;

			quest.ConsumeRequiredItem(player);
			quest.OnComplete(player, frano);
			_completedQuests.Add(CurrentQuestIndex);
			ActiveChestMarker = Vector2.Zero;

			return true;
		}

		/// <summary>
		/// Obtener el mensaje de descripción del quest actual.
		/// </summary>
		public string GetCurrentQuestDialogue()
		{
			if (!HasActiveQuest)
			{
				if (AllQuestsCompleted())
					return "Ya completaste todos mis encargos, sos un crack.";

				return null;
			}

			return CurrentQuest.Description;
		}

		/// <summary>
		/// Obtener el texto del quest actual para la UI.
		/// </summary>
		public string GetCurrentQuestName()
		{
			return HasActiveQuest ? CurrentQuest.Name : null;
		}

		public void SaveData(TagCompound tag)
		{
			tag["questIndex"] = CurrentQuestIndex;
			tag["markerX"] = ActiveChestMarker.X;
			tag["markerY"] = ActiveChestMarker.Y;

			var completedList = new List<int>(_completedQuests);
			tag["completedQuests"] = completedList;
		}

		public void LoadData(TagCompound tag)
		{
			CurrentQuestIndex = tag.GetInt("questIndex");
			float mx = tag.GetFloat("markerX");
			float my = tag.GetFloat("markerY");
			ActiveChestMarker = new Vector2(mx, my);

			if (tag.ContainsKey("completedQuests"))
			{
				var completedList = tag.GetList<int>("completedQuests");
				_completedQuests.Clear();
				foreach (int idx in completedList)
					_completedQuests.Add(idx);
			}
		}

		public void Reset()
		{
			CurrentQuestIndex = -1;
			ActiveChestMarker = Vector2.Zero;
			_completedQuests.Clear();
		}
	}
}
