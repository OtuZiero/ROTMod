using Terraria;
using Terraria.ModLoader;
using ROTMod.Players;
using Microsoft.Xna.Framework;

namespace ROTMod.Commands
{
    public class ROTCommand : ModCommand
    {
        public override string Command => "ROT";
        public override CommandType Type => CommandType.Chat;
        public override string Description => "Usa o ROT para buscar um item.";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0)
            {
                caller.Reply("Use: /ROT <nome do item>", Color.Yellow);
                return;
            }

            Player player = caller.Player;
            var modPlayer = player.GetModPlayer<ROTPlayer>();

            if (!modPlayer.hasROT)
            {
                caller.Reply("Você precisa estar usando a máscara ROT para usar este comando!", Color.Red);
                return;
            }

            string search = string.Join(" ", args);
            modPlayer.ProcessCommand($">ROT {search}");
        }
    }
}