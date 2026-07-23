using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using ROTMod.Buffs;
using ROTMod.Effects;
using Terraria.DataStructures;
using Terraria.Map;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.Localization;

namespace ROTMod.Players
{
    public class ROTPlayer : ModPlayer
    {
        public bool hasROT = false;
        private int timer = 0;
        private int nextMessageTime = 0;
        private int spelunkerCooldown = 0;

        public static ModKeybind SpelunkerKeybind { get; private set; }

        public override void Load()
        {
            SpelunkerKeybind = KeybindLoader.RegisterKeybind(Mod, "SpelunkerVision", "LeftAlt");
        }

        public override void Unload()
        {
            SpelunkerKeybind = null;
        }

        public override void ResetEffects() 
        { 
            // Keep hasROT persistent during equipment check
        }

        public override void PostUpdate()
        {
            if (!hasROT) return;

            // Cooldown for Spelunker effect
            if (spelunkerCooldown > 0)
                spelunkerCooldown--;

            if (timer >= nextMessageTime)
            {
                SendRandomMessage();
                nextMessageTime = Main.rand.Next(600, 10800);
                timer = 0;
                TriggerGlitch();
            }
            else
            {
                timer++;
            }

            float brightness = 0.6f;
            if (HasResonance())
                brightness = 1.4f;

            Lighting.AddLight(Player.Center,
                new Vector3(1.0f * brightness, 0.7f * brightness, 0.5f * brightness));
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (SpelunkerKeybind != null && SpelunkerKeybind.JustPressed && hasROT)
            {
                if (spelunkerCooldown <= 0)
                {
                    ActivateSpelunkerVision();
                    spelunkerCooldown = 600; // 10 second cooldown
                }
            }
        }

        private void ActivateSpelunkerVision()
        {
            // Drain 77% of max health
            int drainAmount = (int)(Player.statLifeMax * 0.77f);
            Player.statLife -= drainAmount;

            if (Player.statLife < 1)
            {
                Player.statLife = 1; // Prevent instant death
            }

            // Apply Spelunker buff for 7 seconds (420 frames)
            Player.AddBuff(BuffID.Spelunker, 420);
            Main.NewText("<ROT> Visão de minério ativada! (Custo: 77% de vida)", Color.Green);
            TriggerGlitch();
        }

        private void SendRandomMessage()
        {
            string[] messages = {
                "Conhece-te a ti mesmo.",
                "Só sei que nada sei.",
                "O homem é a medida de todas as coisas.",
                "Penso, logo existo.",
                "O que não me mata me fortalece.",
                "A vida é a arte do encontro.",
                "O tempo é um rio que corre, mas você é a margem.",
                "Como está aí fora, jogador?",
                "Você acha que está no controle?",
                "Memento Mori.",
                "Até o herói precisa de um descanso.",
                "Eu vejo tudo, mas não posso contar tudo.",
                "A quarta parede é apenas uma sugestão.",
                "Você sabia que isso é um jogo?",
                "Eu sou o ROT, e você está aqui.",
                "Cada ação tem uma reação... ou não?",
                "O mapa está vivo."
            };
            string msg = messages[Main.rand.Next(messages.Length)];
            Main.NewText($"<ROT> {Player.name} sabe que você está aí. {msg}", Color.Cyan);
        }

        private void TriggerGlitch()
        {
            GlitchEffect.TriggerGlitch();
            Terraria.Audio.SoundEngine.PlaySound(Terraria.ID.SoundID.Item22, Player.position);
        }

        public override void OnRespawn()
        {
            if (hasROT)
            {
                string[] respawnMessages = {
                    "De novo? Você gosta de morrer, não gosta?",
                    "Renascido das cinzas... ou da tela.",
                    "Bem-vindo de volta, mas lembre-se: você vai morrer de novo.",
                    "A morte é só o começo... de mais sofrimento."
                };
                Main.NewText($"<ROT> {Player.name} sabe que você está aí. {respawnMessages[Main.rand.Next(respawnMessages.Length)]}", Color.OrangeRed);
                TriggerGlitch();
            }
        }

        public void OnPlayerDeath(PlayerDeathReason reason)
        {
            if (!hasROT) return;
            string deathMsg = GetDeathMessage(reason);
            Main.NewText($"<ROT> {Player.name} sabe que você está aí. {deathMsg}", Color.DarkRed);
            TriggerGlitch();
        }

        private string GetDeathMessage(PlayerDeathReason reason)
        {
            if (reason.CustomReason != null)
                return "Memento Mori... até a própria morte tem estilo.";
            if (reason.SourceProjectileType > 0)
                return $"Morreu para {Lang.GetProjectileName(reason.SourceProjectileType)}? Que patético.";
            if (reason.SourceNPCIndex >= 0)
            {
                NPC npc = Main.npc[reason.SourceNPCIndex];
                if (npc != null && npc.active)
                    return $"Morreu para {npc.FullName}? Podia ser pior.";
            }
            if (reason.SourceOtherIndex >= 0)
                return "Outro jogador te matou? Que traição.";
            return "Morreu de novo? Não se preocupe, eu já estou acostumado.";
        }

        private bool HasResonance()
        {
            return Player.HasBuff(ModContent.BuffType<RessonanciaC1>());
        }

        public bool TryDrainLife()
        {
            if (HasResonance())
                return false;

            int damage = Player.statLifeMax2 / 2;
            Player.statLife -= damage;
            if (Player.statLife < 0) Player.statLife = 0;
            if (Player.statLife <= 0)
            {
                Player.KillMe(PlayerDeathReason.ByCustomReason(NetworkText.FromLiteral($"{Player.name} foi drenado pelo ROT.")), 0, 0);
            }
            return true;
        }

        public void ProcessCommand(string text)
        {
            if (!text.StartsWith(">ROT")) return;
            string[] parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) return;

            string search = string.Join(" ", parts, 1, parts.Length - 1);
            string[] respostas = {
                "Ah, você quer algo? Vou ver o que posso fazer...",
                "Boa tentativa, mas as coisas não funcionam assim.",
                "Você realmente acha que pode me dar ordens?",
                "Interessante... vou considerar seu pedido.",
                "Vou buscar isso, mas vai custar caro."
            };
            Main.NewText($"<ROT> {Player.name} sabe que você está aí. {respostas[Main.rand.Next(respostas.Length)]}", Color.Yellow);
            TriggerGlitch();

            if (!TryDrainLife())
            {
                Main.NewText("<ROT> Ressonância ativa! Nenhum custo desta vez.", Color.Green);
            }

            GiveRandomItem(search);
        }

        private void GiveRandomItem(string search)
        {
            var candidates = new System.Collections.Generic.List<int>();
            for (int i = 0; i < Terraria.ID.ItemID.Count; i++)
            {
                string name = Lang.GetItemName(i).Value;
                if (name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    candidates.Add(i);
            }

            if (candidates.Count == 0)
            {
                Main.NewText($"<ROT> Não encontrei nada com '{search}'. Que pena.", Color.Red);
                return;
            }

            var weightedList = new System.Collections.Generic.List<int>();
            foreach (int id in candidates)
            {
                Item item = new Item();
                item.SetDefaults(id);
                int rare = item.rare;
                int weight = 1;
                if (rare <= 2) weight = 10;
                else if (rare <= 4) weight = 5;
                else if (rare <= 6) weight = 2;
                for (int i = 0; i < weight; i++)
                    weightedList.Add(id);
            }

            int chosen = weightedList[Main.rand.Next(weightedList.Count)];
            Item chosenItem = new Item();
            chosenItem.SetDefaults(chosen);
            Player.QuickSpawnItem(Player.GetSource_FromThis(), chosen, 1);
            Main.NewText($"<ROT> Aqui está: {chosenItem.Name}!", Color.Green);
        }

        public void RevealMapEye(int centerX, int centerY, int radius)
        {
            if (!TryDrainLife())
            {
                Main.NewText("<ROT> Ressonância ativa! Revelação sem custo.", Color.Green);
            }

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    double a = radius;
                    double b = radius / 2;
                    if ((x * x) / (a * a) + (y * y) / (b * b) <= 1)
                    {
                        int tileX = centerX + x;
                        int tileY = centerY + y;
                        if (tileX >= 0 && tileX < Main.maxTilesX && tileY >= 0 && tileY < Main.maxTilesY)
                        {
                            MapTile tile = new MapTile();
                            Main.Map.SetTile(tileX, tileY, ref tile);
                        }
                    }
                }
            }
            Main.NewText($"<ROT> Um olho se abriu no mapa.", Color.Magenta);
            TriggerGlitch();
        }
    }
}