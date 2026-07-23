using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ROTMod.Global
{
    public class GlobalTile : Terraria.ModLoader.GlobalTile
    {
        // Assinatura CORRETA para tModLoader v2026.05
public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
{
    if (fail || effectOnly || noItem) return;

    if (type == TileID.Trees && Main.LocalPlayer.ZoneCrimson)
    {
        if (Main.rand.NextBool(10))
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), new Vector2(i * 16, j * 16), ModContent.ItemType<Items.Cereja>());
        }
    }
}
    }
}