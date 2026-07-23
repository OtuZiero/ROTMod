using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace ROTMod.Global
{
    public class GlobalNPC : Terraria.ModLoader.GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Verifica se o NPC está no bioma Crimson verificando o tile abaixo
            int tileX = (int)(npc.position.X / 16);
            int tileY = (int)((npc.position.Y + npc.height) / 16);
            Tile tile = Framing.GetTileSafely(tileX, tileY);
            
            bool isCrimson = tile.TileType == TileID.CrimsonGrass || 
                             tile.TileType == TileID.Crimstone || 
                             tile.TileType == TileID.Crimsand;

            if (isCrimson)
            {
                // 5% de chance (1 em 20) de dropar uma cereja
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Cereja>(), 20));
            }
        }
    }
}