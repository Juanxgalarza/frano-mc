using Terraria;
using Terraria.ID;

namespace FranoMod.Content.Items.Gorros
{
	[AutoloadEquip(EquipType.Head)]
	public class ElGorritoDePlomo : BaseGorroMineral
	{
		protected override int GorroDefense => 4;
		protected override int BarItemID => ItemID.LeadBar;
		protected override int BarCount => 5;
		protected override int GorroRarity => ItemRarityID.Blue;
		protected override int GorroValue => Item.buyPrice(gold: 1, silver: 50);
	}
}
