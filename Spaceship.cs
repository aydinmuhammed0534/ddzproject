using System;
using System.Collections.Generic;

namespace SpaceWarProject
{
    public class Spaceship : Object
    {
        private const double MOVE_SPEED = 1;
        private const int bullet_speed = 10;
        private const double shoot_down = 0.25; 

        public int Health { get; set; } = 100;
        public double Speed { get; } = 300; // pixels per second
        public List<Bullet> Bullets { get; } = new List<Bullet>();
        public bool IsAlive => Health > 0;

        // PowerUp özellikleri
        public double DamageMultiplier { get; set; } = 1.0;
        public double SpeedMultiplier { get; set; } = 1.0;
        public bool HasShield { get; set; } = false;

        public Spaceship(double x, double y) : base(x, y, 50, 50)
        {
        }

        public void Move(double deltaX, double deltaY)
        {
            // Hız çarpanını uygula
            double adjustedSpeed = MOVE_SPEED * SpeedMultiplier;
            spawnX += deltaX * adjustedSpeed;
            spawnY += deltaY * adjustedSpeed;

            // Ekran sınırlarını kontrol et
            if (spawnX < 0) spawnX = 0;
            if (spawnX > 800 - Width) spawnX = 800 - Width;
            if (spawnY < 0) spawnY = 0;
            if (spawnY > 600 - Height) spawnY = 600 - Height;
        }

        public void Shoot()
        {
            var bullet = new Bullet(
                spawnX + Width,
                spawnY + Height / 2,
                bullet_speed,
                0,
                (int)(20 * DamageMultiplier), // Mermi hasarını DamageMultiplier ile çarp
                false
            );
            Bullets.Add(bullet);
        }

        public void UpdateBullets()
        {
            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                Bullets[i].Move();
                if (Bullets[i].spawnX > 850 || Bullets[i].spawnX < -50 ||
                    Bullets[i].spawnY > 650 || Bullets[i].spawnY < -50)
                {
                    Bullets.RemoveAt(i);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            if (HasShield)
            {
                HasShield = false; // Kalkanı kullan
                return;
            }

            Health -= damage;
            if (Health < 0) Health = 0;
        }
    }
}