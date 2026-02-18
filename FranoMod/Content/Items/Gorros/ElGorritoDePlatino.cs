using Terraria;
using Terraria.ID;

namespace FranoMod.Content.Items.Gorros
{
	[AutoloadEquip(EquipType.Head)]
	public class ElGorritoDePlatino : BaseGorroMineral
	{
		protected override int GorroDefense => 6;
		protected override int BarItemID => ItemID.PlatinumBar;
		protected override int BarCount => 5;
		protected override int GorroRarity => ItemRarityID.Green;
		protected override int GorroValue => Item.buyPrice(gold: 3);
	}
}
