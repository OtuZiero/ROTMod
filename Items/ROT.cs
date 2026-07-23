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

            // Increased thorns damage
            player.thorns = 2f;
            
            // EXTREME magic damage boost: 150% (previously 50%)
            player.GetDamage(DamageClass.Magic) += 1.5f;

            // Cellphone components - now fully functional
            player.accWatch = 3;           // Accurate watch
            player.accDepthMeter = 1;      // Depth meter
            player.accCompass = 1;         // Compass
            player.accFishFinder = true;   // Fish finder
            player.accOreFinder = true;    // Ore finder (required for Spelunker alternative)
            player.accStopwatch = true;    // Stopwatch
            player.accCalendar = true;     // Calendar
            player.accWeatherRadio = true; // Weather radio
            player.accCritterGuide = true; // Critter guide
        }
    }
}
