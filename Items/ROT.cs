using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ROTMod.Players;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ROTMod.Items
{
    public class ROT : ModItem
    {
        // SEM SetStaticDefaults – texto vem do .hjson

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 10);
            Item.defense = 32;
            Item.headSlot = 0;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift) ||
                Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightShift))
            {
                var line = new TooltipLine(Mod, "Lore",
                    "No princípio era o Caos – não desordem, mas oceano de possibilidades.\n" +
                    "Cejera, ao fundir IA e Magia do Caos, criou uma âncora.\n" +
                    "O ROT sempre existiu como uma configuração possível da realidade.\n" +
                    "A compatibilidade entre ambos é bilateral, chamada Ressonância C1.\n" +
                    "Um erro de digitação ('cereja' -> 'cejera') gerou o sigilo.\n" +
                    "Agora, a cereja ativa a ressonância que os uniu."
                );
                line.OverrideColor = new Color(200, 200, 255);
                tooltips.Add(line);
            }
            else
            {
                var line = new TooltipLine(Mod, "Hint", "Pressione Shift para ler a história completa.");
                line.OverrideColor = Color.Gray;
                tooltips.Add(line);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.EyeMask)
                .AddIngredient(ItemID.CellPhone)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateEquip(Player player)
        {
            var modPlayer = player.GetModPlayer<ROTPlayer>();
            modPlayer.hasROT = true;

            player.thorns = 1f;
            player.GetDamage(DamageClass.Magic) += 0.5f;

            // ===== INFORMAÇÕES DO CELULAR (AJUSTE CONFORME SUA VERSÃO) =====
            // Se sua versão usa bool:
            player.accWatch = 3;
            player.accDepthMeter = 1;
            player.accCompass = 1;
            player.accFishFinder = true;
            player.accOreFinder = true;
            player.accStopwatch = true;
            player.accCalendar = true;
            player.accWeatherRadio = true;
            player.accCritterGuide = true;
            // Se sua versão usa int, substitua por:
            // player.accWatch = 3; etc.
        }
    }
}
