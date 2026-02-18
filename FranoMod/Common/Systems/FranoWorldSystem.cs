using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using FranoMod.Common.Quests;
using FranoMod.Content.NPCs;

namespace FranoMod.Common.Systems
{
	/// <summary>
	/// Sistema principal del mundo para el mod de Frano.
	/// Gestiona el estado persistente, el sistema de quests y los marcadores del mapa.
	/// </summary>
	public class FranoWorldSystem : ModSystem
	{
		public bool FranoFreed { get; set; }
		public bool FranoHasSuit { get; set; }
		public bool BoundFranoSpawned { get; set; }

		public QuestManager QuestManager { get; private set; }

		private bool _questsInitialized;

		public override void OnWorldLoad()
		{
			FranoFreed = false;
			FranoHasSuit = false;
			BoundFranoSpawned = false;
			_questsInitialized = false;

			QuestManager = new QuestManager();
			RegisterQuests();
		}

		public override void OnWorldUnload()
		{
			FranoFreed = false;
			FranoHasSuit = false;
			BoundFranoSpawned = false;
			_questsInitialized = false;
			QuestManager = null;
		}

		/// <summary>
		/// Registrar todos los quests aquí. Para agregar un quest nuevo,
		/// simplemente agregar una línea RegisterQuest con la nueva instancia.
		/// </summary>
		private void RegisterQuests()
		{
			QuestManager.RegisterQuest(new EscaparDeLaLey());
			QuestManager.RegisterQuest(new TraerLaRopa());
			QuestManager.RegisterQuest(new BuscarElCelular());
		}

		public override void PostUpdateWorld()
		{
			// Spawnear BoundFrano en la superficie si no fue liberado y no está presente
			if (!FranoFreed && !BoundFranoSpawned && Main.netMode != Terraria.ID.NetmodeID.MultiplayerClient)
			{
				if (!NPC.AnyNPCs(ModContent.NPCType<BoundFrano>()) &&
					!NPC.AnyNPCs(ModContent.NPCType<Frano>()))
				{
					SpawnBoundFrano();
				}
			}

			// Iniciar el primer quest automáticamente cuando Frano es liberado
			if (FranoFreed && !_questsInitialized && QuestManager != null)
			{
				if (!QuestManager.HasActiveQuest && !QuestManager.IsQuestCompleted(0))
				{
					QuestManager.StartNextQuest();
				}
				_questsInitialized = true;
			}
		}

		private void SpawnBoundFrano()
		{
			// Buscar posición en la superficie cerca del spawn
			int spawnX = Main.spawnTileX + Main.rand.Next(-80, 80);
			int spawnY = 0;

			// Encontrar el suelo en esa posición X
			for (int y = 10; y < Main.worldSurface; y++)
			{
				if (Main.tile[spawnX, y].HasTile && Main.tileSolid[Main.tile[spawnX, y].TileType])
				{
					spawnY = y - 1;
					break;
				}
			}

			if (spawnY > 0)
			{
				int npcIndex = NPC.NewNPC(
					Terraria.Entity.GetSource_NaturalSpawn(),
					spawnX * 16, spawnY * 16,
					ModContent.NPCType<BoundFrano>()
				);

				if (npcIndex >= 0)
					BoundFranoSpawned = true;
			}
		}

		public override void SaveWorldData(TagCompound tag)
		{
			tag["franoFreed"] = FranoFreed;
			tag["franoHasSuit"] = FranoHasSuit;
			tag["boundFranoSpawned"] = BoundFranoSpawned;

			if (QuestManager != null)
				QuestManager.SaveData(tag);
		}

		public override void LoadWorldData(TagCompound tag)
		{
			FranoFreed = tag.GetBool("franoFreed");
			FranoHasSuit = tag.GetBool("franoHasSuit");
			BoundFranoSpawned = tag.GetBool("boundFranoSpawned");

			QuestManager ??= new QuestManager();
			RegisterQuests();
			QuestManager.LoadData(tag);

			_questsInitialized = FranoFreed;
		}

		/// <summary>
		/// Dibuja el marcador del cofre del quest en el mapa completo.
		/// </summary>
		public override void PostDrawFullscreenMap(ref string mouseText)
		{
			if (QuestManager == null || QuestManager.ActiveChestMarker == Vector2.Zero)
				return;

			Vector2 chestWorld = QuestManager.ActiveChestMarker;
			float chestTileX = chestWorld.X / 16f;
			float chestTileY = chestWorld.Y / 16f;

			// Convertir coordenadas de tile a coordenadas del mapa fullscreen
			float mapScale = Main.mapFullscreenScale;
			Vector2 mapCenter = Main.mapFullscreenPos;
			float screenCenterX = Main.screenWidth / 2f;
			float screenCenterY = Main.screenHeight / 2f;

			float mapX = (chestTileX - mapCenter.X) * mapScale + screenCenterX;
			float mapY = (chestTileY - mapCenter.Y) * mapScale + screenCenterY;

			// Verificar si está visible en el mapa
			if (mapX < -20 || mapY < -20 || mapX > Main.screenWidth + 20 || mapY > Main.screenHeight + 20)
				return;

			// Dibujar un marcador simple (cuadrado rojo con borde)
			var markerRect = new Rectangle((int)mapX - 6, (int)mapY - 6, 12, 12);
			var borderRect = new Rectangle((int)mapX - 7, (int)mapY - 7, 14, 14);

			SpriteBatch spriteBatch = Main.spriteBatch;
			Texture2D pixel = Terraria.GameContent.TextureAssets.MagicPixel.Value;

			spriteBatch.Draw(pixel, borderRect, Color.Black);
			spriteBatch.Draw(pixel, markerRect, Color.Yellow);

			// Tooltip al pasar el mouse
			if (markerRect.Contains(Main.mouseX, Main.mouseY))
			{
				string questName = QuestManager.GetCurrentQuestName() ?? "Quest";
				mouseText = $"[Quest] {questName}";
			}
		}
	}
}
