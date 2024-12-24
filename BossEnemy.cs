using System;
using System.Collections.Generic;

namespace SpaceWarProject
{
    public class BossEnemy : Enemy
    {
        private const double move_speed = 2;
        private const int enemy_health = 120;
        private double moveTimer = 0;
        private double attackPhase = 0;
        private Random random = new Random();

        public BossEnemy(double startX, double startY) 
            : base(startX, startY, 60, 60, "Boss")
        {
            health = enemy_health;
            speedX = -move_speed;
            ScoreValue = 50;
        }

        public override void Move()
        {
            moveTimer += 0.02;
            
            speedX = -move_speed + Math.Sin(moveTimer * 0.5) * 2;
            
            double verticalAmplitude = 150;
            spawnY = 300 + Math.Sin(moveTimer) * verticalAmplitude * Math.Cos(moveTimer * 0.5);
            
            spawnX += speedX;

            if (spawnX < 400) spawnX = 400; 
            if (spawnY < 0) spawnY = 0;
            if (spawnY > 600 - Height) spawnY = 600 - Height;
        }

        public override void Attack()
        {
            attackPhase += 0.1;
            
            switch ((int)(attackPhase * 2) % 3)
            {
               
                case 1: 
                    WaveAttack();
                    break;
                case 2: 
                    TargetedAttack();
                    break;
            }
        }

        // private void attacktocircular()
        // {
        //     
        //     int bulletCount = 12;
        //     for (int i = 0; i < bulletCount; i++)
        //     {
        //         double angle = (360.0 / bulletCount) * i;
        //         double radians = angle * Math.PI / 180.0;
        //         double bulletSpeedX = -bullet_speed * Math.Cos(radians);
        //         double bulletSpeedY = bullet_speed * Math.Sin(radians);
        //         var bullet = new Bullet(spawnX + Width/2, spawnY + Height/2, bulletSpeedX, bulletSpeedY, 20, true);
        //         Bullets.Add(bullet);
        //     }
        // }

        private void WaveAttack()
        {
            double baseAngle = Math.Sin(attackPhase * 2) * 30;
            double[] angles = { baseAngle - 20, baseAngle - 10, baseAngle, baseAngle + 10, baseAngle + 20 };
            foreach (var angle in angles)
            {
                double radians = angle * Math.PI / 180.0;
                double bulletSpeedX = -bullet_speed * Math.Cos(radians);
                double bulletSpeedY = bullet_speed * Math.Sin(radians);
                var bullet = new Bullet(spawnX, spawnY + Height/2, bulletSpeedX, bulletSpeedY, 15, true);
                Bullets.Add(bullet);
            }
        }

        private void TargetedAttack()
        {
            var player = Game.Current.PlayerShip;
            if (player != null)
            {
                double deltaX = player.spawnX - spawnX;
                double deltaY = player.spawnY - spawnY;
                double angle = Math.Atan2(deltaY, -deltaX);
                
                double bulletSpeedX = -bullet_speed * Math.Cos(angle);
                double bulletSpeedY = bullet_speed * Math.Sin(angle);
                var bullet = new Bullet(spawnX, spawnY + Height/2, bulletSpeedX, bulletSpeedY, 25, true);
                Bullets.Add(bullet);

                for (int i = -1; i <= 1; i++)
                {
                    if (i == 0) continue;
                    double spreadAngle = angle + (i * Math.PI / 6);
                    bulletSpeedX = -bullet_speed * Math.Cos(spreadAngle);
                    bulletSpeedY = bullet_speed * Math.Sin(spreadAngle);
                    bullet = new Bullet(spawnX, spawnY + Height/2, bulletSpeedX, bulletSpeedY, 15, true);
                    Bullets.Add(bullet);
                }
            }
        }
    }
}
