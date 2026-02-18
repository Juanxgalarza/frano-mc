using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items
{
	public class ElTraje : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 1;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Green;
		}
	}
}
