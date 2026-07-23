using Terraria;
using Terraria.ModLoader;

namespace ROTMod.Buffs
{
    public class RessonanciaC1 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName e Description vêm do .hjson
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = false;
        }
    }
}