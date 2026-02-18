using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using FranoMod.Common.Systems;

namespace FranoMod.Content.NPCs
{
	/// <summary>
	/// Versión atada de Frano que aparece en la superficie al iniciar el juego.
	/// Aparece en shorts y sin remera, atado como el Goblin Tinkerer.
	/// Al liberarlo, da una mini-recompensa y permite que Frano aparezca como town NPC.
	/// </summary>
	public class BoundFrano : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = 1;
			NPCID.Sets.NoTownNPCHappiness[Type] = true;
			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = bestiaryData;
		}

		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = 0; // Estático, no se mueve
			NPC.friendly = true;
			NPC.dontTakeDamage = true;
		}

		public override bool CanChat() => true;

		public override string GetChat()
		{
			return "¡Eh, loco! ¡Ayudame que estoy atado acá! Hacé clic en 'Liberar' y te doy algo a cambio.";
		}

		public override void SetChatButtons(ref string button1, ref string button2)
		{
			button1 = "Liberar";
		}

		public override void OnChatButtonClicked(bool firstButton, ref string shopName)
		{
			if (!firstButton)
				return;

			var system = ModContent.GetInstance<FranoWorldSystem>();
			system.FranoFreed = true;

			// Mini-recompensa al liberar a Frano
			Player player = Main.LocalPlayer;
			player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ItemID.GoldCoin, 2);
			player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ItemID.HealingPotion, 5);
			player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ItemID.Torch, 30);

			Main.npcChatText = "¡Gracias, capo! Me salvaste la vida. Voy a buscar un lugar para instalarme. " +
				"Pasá por mi tienda que te voy a tener cosas buenas.";

			// Spawnear Frano como town NPC
			NPC.NewNPC(
				NPC.GetSource_NaturalSpawn(),
				(int)NPC.Center.X, (int)NPC.Center.Y,
				ModContent.NPCType<Frano>()
			);

			// Desaparecer
			NPC.active = false;
			NPC.netUpdate = true;

			if (Main.netMode == NetmodeID.Server)
				NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
		}
	}
}
