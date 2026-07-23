using Terraria;
using Terraria.ModLoader;

namespace ROTMod.Buffs
{
    public class RessonanciaC1 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.AddTranslation("en-US", "Ressonância C1");
            Description.AddTranslation("en-US", "Você está em sintonia com o ROT!\nNenhum custo de vida para usar suas habilidades.");
        }
    }
}
