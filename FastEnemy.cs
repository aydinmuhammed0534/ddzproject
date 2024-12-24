using System;

namespace SpaceWarProject
{
    public class FastEnemy : Enemy
    {
        private const double move_speed = 7;
        private const int enemy_health = 30;
        private double verticalDirection = 1;
        private double directionChangeTimer = 0;

        public FastEnemy(double startX, double startY) 
            : base(startX, startY, 25, 25, "Fast")
        {
            health = enemy_health;
            speedX = -move_speed;
            ScoreValue = 20;
        }

        public override void Move()
        {
           

            speedY = verticalDirection * move_speed;
            spawnX += speedX;
            spawnY += speedY;

            if (spawnY < 0)
            {
                spawnY = 0;
                verticalDirection = 1;
            }
            if (spawnY > 600 - Height)
            {
                spawnY = 600 - Height;
                verticalDirection = -1;
            }
        }

        public override void Attack()
        {
            double[] angles = { -10, 10 };
            foreach (var angle in angles)
            {
                double radians = angle * Math.PI / 180.0;
                double bulletSpeedX = -bullet_speed * 1.2 * Math.Cos(radians);
                double bulletSpeedY = bullet_speed * 1.2 * Math.Sin(radians);
                var bullet = new Bullet(spawnX, spawnY + Height / 2, bulletSpeedX, bulletSpeedY, 7, true);
                Bullets.Add(bullet);
            }
        }
    }
}