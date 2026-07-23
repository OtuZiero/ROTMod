using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ROTMod.Systems
{
    public static class ROTEyeSystem
    {
        public class Eye
        {
            public Vector2 Position;
            public float Scale;
            public int TimeLeft;
        }

        public static readonly List<Eye> Eyes = new();

        public static void AddEye(Vector2 position)
        {
            Eyes.Add(new Eye()
            {
                Position = position,
                Scale = 1f,
                TimeLeft = 1800 //30 segundos
            });
        }

        public static void Update()
        {
            for (int i = Eyes.Count - 1; i >= 0; i--)
            {
                Eyes[i].TimeLeft--;

                Eyes[i].Scale =
                    1f +
                    (float)System.Math.Sin(1 * .08f) * .06f;

                if (Eyes[i].TimeLeft <= 0)
                    Eyes.RemoveAt(i);
            }
        }
    }
}