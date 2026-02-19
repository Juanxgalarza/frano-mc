using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using FranoMod.Common.Systems;
using FranoMod.Content.Items;

namespace FranoMod.Content.NPCs
{
	/// <summary>
	/// Frano - Town NPC con tienda y sistema de quests.
	/// Empieza en shorts sin remera. Tras el quest "Traer la ropa" se viste de traje smoking.
	/// Tiene una tienda que se expande según los quests completados.
	/// </summary>
	[AutoloadHead]
	public class Frano : ModNPC
	{
		private const string ShopName = "Tienda de Frano";

		// Textura alternativa para cuando tiene el traje
		private Asset<Texture2D> _suitedTexture;

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = 25; // Frames estándar de town NPC
			NPCID.Sets.ExtraFramesCount[Type] = 9;
			NPCID.Sets.AttackFrameCount[Type] = 4;
			NPCID.Sets.DangerDetectRange[Type] = 700;
			NPCID.Sets.AttackType[Type] = 3; // Melee (látigo) - cambia a ranged con traje
			NPCID.Sets.AttackTime[Type] = 90;
			NPCID.Sets.AttackAverageChance[Type] = 30;
			NPCID.Sets.HatOffsetY[Type] = 4;

			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new()
			{
				Velocity = 1f,
				Direction = 1
			};
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = bestiaryData;
		}

		public override void SetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = 7; // Town NPC AI
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			AnimationType = NPCID.Guide;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement(
					"Frano es un tipo misterioso que apareció atado en la superficie. " +
					"Nadie sabe bien de dónde viene, pero tiene buenos contactos y una tienda variada."
				)
			});
		}

		public override bool CanTownNPCSpawn(int numTownNPCs)
		{
			var system = ModContent.GetInstance<FranoWorldSystem>();
			return system != null && system.FranoFreed;
		}

		public override List<string> SetNPCNameList()
		{
			return new List<string> { "Frano" };
		}

		public override ITownNPCProfile TownNPCProfile()
		{
			return new FranoProfile();
		}

		public override string GetChat()
		{
			var system = ModContent.GetInstance<FranoWorldSystem>();
			var questMgr = system?.QuestManager;

			WeightedRandom<string> chat = new();

			// Diálogos generales
			chat.Add("¿Qué onda, loco? ¿Necesitás algo?");
			chat.Add("La vida en este mundo es dura, pero acá estamos.");
			chat.Add("Si necesitás algo, revisá mi tienda.");

			if (system != null && system.FranoHasSuit)
			{
				chat.Add("¿Viste qué bien queda el traje? Ahora sí parezco gente.", 2.0);
				chat.Add("Con Pesto en la mano me siento más seguro.");
			}
			else
			{
				chat.Add("Necesito conseguir algo de ropa, no puedo andar así para siempre...");
			}

			// Si hay un quest activo, dar pista
			if (questMgr != null && questMgr.HasActiveQuest)
			{
				chat.Add(questMgr.GetCurrentQuestDialogue(), 3.0);
			}

			return chat;
		}

		public override void SetChatButtons(ref string button1, ref string button2)
		{
			button1 = Language.GetTextValue("LegacyInterface.28"); // "Shop" / "Tienda"
			button2 = "Quest";
		}

		public override void OnChatButtonClicked(bool firstButton, ref string shopName)
		{
			if (firstButton)
			{
				shopName = ShopName;
				return;
			}

			// Botón de Quest
			var system = ModContent.GetInstance<FranoWorldSystem>();
			var questMgr = system?.QuestManager;

			if (questMgr == null)
			{
				Main.npcChatText = "Algo salió mal con mis encargos...";
				return;
			}

			// Intentar completar quest actual
			if (questMgr.HasActiveQuest)
			{
				if (questMgr.TryCompleteCurrentQuest(Main.LocalPlayer, NPC))
				{
					Main.npcChatText = questMgr.CurrentQuest != null
						? questMgr.CurrentQuest.CompletionMessage
						: "¡Bien hecho!";

					// Verificar si el quest completado era el anterior
					// e intentar iniciar el siguiente
					if (!questMgr.AllQuestsCompleted())
					{
						// El siguiente quest se inicia cuando el jugador vuelva a hablar
					}
				}
				else
				{
					Main.npcChatText = questMgr.GetCurrentQuestDialogue()
						?? "Todavía no tenés lo que necesito. ¡Fijate en el mapa!";
				}
			}
			else if (!questMgr.AllQuestsCompleted())
			{
				// Iniciar siguiente quest
				if (questMgr.StartNextQuest())
				{
					Main.npcChatText = questMgr.GetCurrentQuestDialogue()
						?? "Tengo un encargo nuevo para vos.";
				}
				else
				{
					Main.npcChatText = "Mmm, no encontré un buen lugar para el encargo. Intentá más tarde.";
				}
			}
			else
			{
				Main.npcChatText = "Ya completaste todos mis encargos. ¡Sos un crack, loco!";
			}
		}

		public override void AddShops()
		{
			var system = ModContent.GetInstance<FranoWorldSystem>();

			// Condiciones basadas en progreso de quests
			Condition quest1Done = new("Mods.FranoMod.Conditions.Quest1Done",
				() => system?.QuestManager?.IsQuestCompleted(0) ?? false);

			Condition quest2Done = new("Mods.FranoMod.Conditions.Quest2Done",
				() => system?.QuestManager?.IsQuestCompleted(1) ?? false);

			new NPCShop(Type, ShopName)
				// Items base (siempre disponibles)
				.Add(ItemID.Gel)
				.Add(ItemID.Torch)
				.Add(ItemID.Rope)

				// Items tras completar Quest 1: "Escapar de la ley"
				.Add(ItemID.Shuriken, quest1Done)
				.Add(ItemID.ThrowingKnife, quest1Done)
				.Add(ItemID.Grenade, quest1Done)
				.Add(ItemID.Bomb, quest1Done)
				.Add(ItemID.IronBar, quest1Done)

				// Items tras completar Quest 2: "Traer la ropa"
				.Add(ModContent.ItemType<BalaPesto>(), quest2Done)
				.Add(ItemID.MusketBall, quest2Done)
				.Add(ItemID.SilverBullet, quest2Done)

				.Register();
		}

		/// <summary>
		/// Intercambia la textura de Frano si tiene el traje.
		/// </summary>
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			var system = ModContent.GetInstance<FranoWorldSystem>();
			if (system != null && system.FranoHasSuit)
			{
				// Cargar textura del traje si no está cargada
				_suitedTexture ??= ModContent.Request<Texture2D>("FranoMod/Content/NPCs/Frano_Suited");

				if (_suitedTexture != null && _suitedTexture.IsLoaded)
				{
					// Usar textura del traje
					TextureAssets.Npc[Type] = _suitedTexture;
				}
			}

			return true;
		}

		/// <summary>
		/// Cambia dinámicamente el tipo de ataque según si tiene el traje.
		/// Sin traje: melee con látigo de cuero. Con traje: ranged con Pesto.
		/// </summary>
		public override void PostAI()
		{
			var system = ModContent.GetInstance<FranoWorldSystem>();
			if (system != null && system.FranoHasSuit)
			{
				NPCID.Sets.AttackType[Type] = 1; // Ranged (pistola)
				NPCID.Sets.AttackTime[Type] = 90;
			}
			else
			{
				NPCID.Sets.AttackType[Type] = 3; // Melee (látigo)
				NPCID.Sets.AttackTime[Type] = 30;
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			var system = ModContent.GetInstance<FranoWorldSystem>();
			if (system != null && system.FranoHasSuit)
			{
				damage = 20;
				knockback = 4f;
			}
			else
			{
				damage = 15;
				knockback = 6f;
			}
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 30;
			randExtraCooldown = 30;
		}

		// --- Ataque ranged (solo activo con traje, AttackType = 1) ---

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ModContent.ProjectileType<Projectiles.PestoProjectile>();
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}

		public override void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
		{
			int pestoType = ModContent.ItemType<Pesto>();
			Main.instance.LoadItem(pestoType);
			item = TextureAssets.Item[pestoType].Value;
			itemFrame = item.Frame();
			scale = 0.8f;
			horizontalHoldoutOffset = -4;
		}

		// --- Ataque melee con látigo (activo sin traje, AttackType = 3) ---

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = 40;
			itemHeight = 40;
		}

		public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
		{
			Main.instance.LoadItem(ItemID.BlandWhip);
			item = TextureAssets.Item[ItemID.BlandWhip].Value;
			itemFrame = item.Frame();
			itemSize = 40;
			scale = 0.6f;
		}
	}

	/// <summary>
	/// Profile para el NPC Frano. Gestiona las texturas del head icon en el mapa.
	/// </summary>
	public class FranoProfile : ITownNPCProfile
	{
		public int RollVariation() => 0;

		public string GetNameForVariant(NPC npc) => "Frano";

		public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
		{
			var system = ModContent.GetInstance<FranoWorldSystem>();
			if (system != null && system.FranoHasSuit)
			{
				return ModContent.Request<Texture2D>("FranoMod/Content/NPCs/Frano_Suited");
			}

			return ModContent.Request<Texture2D>("FranoMod/Content/NPCs/Frano");
		}

		public int GetHeadTextureIndex(NPC npc)
		{
			return ModContent.GetModHeadSlot("FranoMod/Content/NPCs/Frano_Head");
		}
	}
}
