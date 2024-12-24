using System.Collections.Generic;

namespace SpaceWarProject
{
    public class PowerUp : Object
    {
        public enum PowerUpType
        {
            Health,
            Shield,
            DoubleDamage,
            Speed
        }

        public PowerUpType Type { get; private set; }
        public bool IsActive { get; set; } = true;
        private double duration = 5.0; // 
        private double activeTime = 0;
        private bool effectActive = false;
        private Spaceship? player = null;

        public PowerUp(double x, double y, PowerUpType type) 
            : base(x, y, 20, 20) // 
        {
            Type = type;
        }

        public void ApplyPowerUp(Spaceship spaceship)
        {
            if (!IsActive || effectActive) return;

            player = spaceship;

            switch (Type)
            {
                case PowerUpType.Health:
                    player.TakeDamage(-50); 
                    break;
                case PowerUpType.DoubleDamage:
                    player.DamageMultiplier = 2.0;
                    effectActive = true;
                    break;
                case PowerUpType.Shield:
                    player.HasShield = true;
                    effectActive = true;
                    break;
                case PowerUpType.Speed:
                    player.SpeedMultiplier = 1.2;
                    effectActive = true;
                    break;
            }
            IsActive = false;
        }

        public void Update(double deltaTime)
        {
            if (!IsActive) return;

            spawnY += 1;

            if (spawnY > 600)
            {
                IsActive = false;
                return;
            }

            if (effectActive)
            {
                activeTime += deltaTime;
                if (activeTime >= duration)
                {
                    RemoveEffect();
                }
            }
        }

        private void RemoveEffect()
        {
            if (player == null) return;
            
            effectActive = false;
            activeTime = 0;
            
            switch (Type)
            {
                case PowerUpType.DoubleDamage:
                    player.DamageMultiplier = 1.0;
                    break;
                case PowerUpType.Shield:
                    player.HasShield = false;
                    break;
                case PowerUpType.Speed:
                    player.SpeedMultiplier = 1.0;
                    break;
            }
            
            player = null;
        }
    }
}