using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ROTMod.Items
{
    [AutoloadEquip(EquipType.Head)]
    public class ROT_Head : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.defense = 32;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 10);
        }
    }
}
