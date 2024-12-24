using System;

namespace SpaceWarProject
{
    public class StrongEnemy : Enemy
    {
        private const double move_speed = 3;
        private const int enemy_health = 100;
        private double timer = 0;
        private double baseY;

        public StrongEnemy(double startX, double startY) 
            : base(startX, startY, 45, 45, "Strong")
        {
            health = enemy_health;
            speedX = -move_speed;
            ScoreValue = 30;
            baseY = startY;
        }

        public override void Move()
        {
            timer += 0.02;
            speedY = Math.Sin(timer) * 3;
            
            spawnX += speedX;
            spawnY = baseY + Math.Sin(timer) * 100; 
            if (spawnY < 0) spawnY = 0;
            if (spawnY > 600 - Height) spawnY = 600 - Height;
        }

        public override void Attack()
        {
            double[] angles = { -20, -10, 0 };
            foreach (var angle in angles)
            {
                double radians = angle * Math.PI / 180.0;
                double bulletSpeedX = -bullet_speed * 0.8 * Math.Cos(radians);
                double bulletSpeedY = bullet_speed * 0.8 * Math.Sin(radians);
                var bullet = new Bullet(spawnX, spawnY + Height / 2, bulletSpeedX, bulletSpeedY, 15, true);
                Bullets.Add(bullet);
            }
        }
    }
}