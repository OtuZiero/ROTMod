using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ROTMod.Players;
using System.Collections.Generic;

namespace ROTMod.Items
{
    public class Cereja : ModItem
    {
        // SEM SetStaticDefaults

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 50);
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.maxStack = 999;
            Item.buffType = ModContent.BuffType<Buffs.RessonanciaC1>();
            Item.buffTime = 420;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Apple, 1)
                .AddIngredient(ItemID.CrimstoneBlock, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            if (player == null) return;

            var modPlayer = player.GetModPlayer<ROTPlayer>();
            bool hasROT = modPlayer != null && modPlayer.hasROT;

            foreach (TooltipLine line in tooltips)
            {
                if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                {
                    if (hasROT)
                    {
                        line.Text = "Fruta vermelha que ativa a Ressonância C1.\n" +
                                    "O ROT reconhece sua assinatura.\n" +
                                    "Efeito: 7 segundos sem custo de vida para o ROT.";
                    }
                    else
                    {
                        line.Text = "Uma fruta doce e vermelha.";
                    }
                    break;
                }
            }
        }
    }
}