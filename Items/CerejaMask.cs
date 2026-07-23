using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ROTMod.Items
{
    public class CerejaMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 50);
        }
    }
}
