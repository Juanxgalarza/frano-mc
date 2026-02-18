using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items
{
	/// <summary>
	/// Item de quest: el celular nuevo de Frano, perdido en un cofre.
	/// </summary>
	public class CelularDeFrano : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 28;
			Item.maxStack = 1;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
