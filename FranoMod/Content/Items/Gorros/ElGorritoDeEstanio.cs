using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Items.Gorros
{
	[AutoloadEquip(EquipType.Head)]
	public class ElGorritoDeEstanio : BaseGorroMineral
	{
		protected override int GorroDefense => 3;
		protected override int BarItemID => ItemID.TinBar;
		protected override int BarCount => 5;
		protected override int GorroRarity => ItemRarityID.White;
		protected override int GorroValue => Item.buyPrice(gold: 1, silver: 20);
	}
}
