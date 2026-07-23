using Terraria;

namespace ROTMod.Effects
{
    public static class GlitchEffect
    {
        // Duração restante do efeito
        private static int _timer;

        // Tempo até mudar o estado do glitch
        private static int _blinkTimer;

        // Se o glitch deve ser renderizado neste frame
        public static bool IsGlitching { get; private set; }

        // Intensidade opcional para shaders
        public static float Intensity { get; private set; }

        public static void TriggerGlitch(int duration = 60)
        {
            // Reinicia/renova o efeito
            _timer = duration;

            // Faz aparecer imediatamente
            IsGlitching = true;
            Intensity = Main.rand.NextFloat(0.5f, 1f);

            // Próxima mudança em poucos frames
            _blinkTimer = Main.rand.Next(2, 6);
        }

        public static void Update()
        {
            if (_timer <= 0)
            {
                IsGlitching = false;
                Intensity = 0f;
                return;
            }

            _timer--;

            _blinkTimer--;

            if (_blinkTimer <= 0)
            {
                // Alterna ligado/desligado
                IsGlitching = !IsGlitching;

                // Nova intensidade aleatória
                Intensity = IsGlitching
                    ? Main.rand.NextFloat(0.4f, 1f)
                    : 0f;

                // Intervalo aleatório até a próxima piscada
                _blinkTimer = Main.rand.Next(1, 5);
            }
        }
    }
}