using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using ROTMod.Players;
using ROTMod.Effects;
using System;
using System.Reflection;
using MonoMod.RuntimeDetour;
using Terraria.DataStructures;
using Terraria.Map; // Para MapTile (mas não é usado aqui)

namespace ROTMod.Global
{
    public class ROTGlobal : ModSystem
    {
        private static Hook killMeHook;

        public override void Load()
        {
            MethodInfo killMeMethod = typeof(Player).GetMethod("KillMe", new Type[] { typeof(PlayerDeathReason), typeof(double), typeof(int), typeof(bool) });
            if (killMeMethod == null) throw new Exception("Método Player.KillMe não encontrado!");
            killMeHook = new Hook(killMeMethod, typeof(ROTGlobal).GetMethod("KillMeHook", BindingFlags.Public | BindingFlags.Static));
        }

        public override void Unload()
        {
            killMeHook?.Dispose();
        }

        public static void KillMeHook(Action<Player, PlayerDeathReason, double, int, bool> orig, Player player, PlayerDeathReason reason, double dmg, int hitDirection, bool pvp)
        {
            var modPlayer = player.GetModPlayer<ROTPlayer>();
            if (modPlayer.hasROT)
                modPlayer.OnPlayerDeath(reason);
            orig(player, reason, dmg, hitDirection, pvp);
        }

        public override void PostUpdateEverything()
        {
            GlitchEffect.Update();

            if (Main.LocalPlayer == null)
                return;

            var modPlayer = Main.LocalPlayer.GetModPlayer<ROTPlayer>();

            if (!modPlayer.hasROT)
                return;

            if (Main.mapFullscreen &&
                Main.mouseLeft &&
                Main.mouseLeftRelease)
            {
                Main.mouseLeftRelease = false;

                Vector2 mapPos =
                    Main.mapFullscreenPos +
                    new Vector2(Main.mouseX, Main.mouseY) /
                    Main.mapFullscreenScale;


                GlitchEffect.TriggerGlitch();
            }
            
        }

        public override void PostDrawTiles()
        {
            if (GlitchEffect.IsGlitching)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                for (int i = 0; i < 30; i++)
                {
                    int x = Main.rand.Next(Main.screenWidth);
                    int y = Main.rand.Next(Main.screenHeight);
                    int w = Main.rand.Next(10, 60);
                    int h = Main.rand.Next(5, 25);
                    Color color = new Color(Main.rand.Next(0, 255), Main.rand.Next(0, 255), Main.rand.Next(0, 255), 150);
                    Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(x, y, w, h), color);
                }
                for (int i = 0; i < 5; i++)
                {
                    int y = Main.rand.Next(Main.screenHeight);
                    int h2 = Main.rand.Next(2, 8);
                    Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, y, Main.screenWidth, h2), Color.White * 0.5f);
                }
                Main.spriteBatch.End();
            }
        }
    }
}