using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items
{
	public class OrdenDeDetencion : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 1;
			Item.value = Item.buyPrice(silver: 50);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
