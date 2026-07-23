using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ROTMod.Players;

namespace ROTMod.Items
{
    public class CerejaGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Only modify Terraria's cherry fruit
            if (item.type == Terraria.ID.ItemID.Cherry)
            {
                var rotLine = new TooltipLine(Mod, "ROTMod", 
                    "Ativa Ressonância C1 com a Máscara ROT\n" +
                    "Grant 2 minutes of Ressonancia buff when used with ROT Mask equipped")
                {
                    OverrideColor = new Color(200, 100, 255)
                };
                tooltips.Add(rotLine);
            }
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            if (item.type == Terraria.ID.ItemID.Cherry)
            {
                var modPlayer = player.GetModPlayer<ROTPlayer>();

                if (modPlayer.hasROT)
                {
                    player.AddBuff(
                        ModContent.BuffType<Buffs.RessonanciaC1>(),
                        7200);

                    Terraria.Audio.SoundEngine.PlaySound(
                        Terraria.ID.SoundID.Item4,
                        player.position);
                }
            }

            return true; // permite que a cereja seja consumida normalmente
        }
    }
}
