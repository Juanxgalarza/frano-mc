using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FranoMod.Content.Tiles
{
	public class FraniumOreTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileOreFinderPriority[Type] = 620;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileShine[Type] = 1050;
			Main.tileShine2[Type] = true;

			AddMapEntry(new Color(85, 70, 140), CreateMapEntryName());

			DustType = DustID.PurpleTorch;
			HitSound = SoundID.Tink;
			MineResist = 2f;
			MinPick = 55;

			RegisterItemDrop(ModContent.ItemType<Items.FraniumOre>());
		}
	}
}
