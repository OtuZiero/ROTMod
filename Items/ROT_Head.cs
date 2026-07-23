using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ROTMod.Items
{
    [AutoloadEquip(EquipType.Head)]
    public class ROT_Head : ModItem
    {
        // Animation state
        private static int blinkTimer = 0;
        private static int nextBlinkInterval = 0;
        private static bool isBlinked = false;

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.defense = 32;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 10);
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            base.Update(ref gravity, ref maxFallSpeed);
            UpdateAnimationState();
        }

        private static void UpdateAnimationState()
        {
            blinkTimer++;

            // Set next blink interval (5-10 seconds = 300-600 frames at 60 FPS)
            if (nextBlinkInterval == 0)
            {
                nextBlinkInterval = Main.rand.Next(300, 600);
                isBlinked = false;
            }

            // Trigger blink at random interval
            if (blinkTimer >= nextBlinkInterval)
            {
                isBlinked = !isBlinked;
                blinkTimer = 0;
                nextBlinkInterval = 0;
            }
        }

        public static bool IsBlinked()
        {
            return isBlinked;
        }
    }
}