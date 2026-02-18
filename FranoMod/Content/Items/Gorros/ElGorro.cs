using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items.Gorros
{
	/// <summary>
	/// El Gorro: beanie base que Frano te regala al completar su quest del celular.
	/// Buena defensa para el inicio del juego. Se puede mejorar con minerales en un anvil.
	/// </summary>
	[AutoloadEquip(EquipType.Head)]
	public class ElGorro : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.defense = 2;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
