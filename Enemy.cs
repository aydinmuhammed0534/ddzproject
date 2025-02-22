using System;
using System.Collections.Generic;

namespace SpaceWarProject
{
    public abstract class Enemy : Object
    {
        protected double speedX = 0;
        protected double speedY = 0;
        protected int health;
        protected double shootCooldown = 0;
        protected const double shoot_interval = 0.5; 
        protected const double bullet_speed = 9;

        public int Health { get => health; set => health = value; }
        public List<Bullet> Bullets { get; } = new List<Bullet>();
        public int ScoreValue { get; protected set; }
        public bool IsAlive => Health > 0;
        public string EnemyType { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }

        public Enemy(double startX, double startY, double width, double height, string type) 
            : base(startX, startY, width, height)
        {
            Width = width;
            Height = height;
            EnemyType = type;
            health = 50;
            shootCooldown = 0;
            ScoreValue = 10;
        }

        public void Update(double deltaTime)
        {
            Move();
            
            shootCooldown -= deltaTime;
            if (shootCooldown <= 0)
            {
                Attack();
                shootCooldown = shoot_interval;
            }

            UpdateBullets();
        }

        protected void UpdateBullets()
        {
            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                Bullets[i].Move();
                if (Bullets[i].spawnX < 0 || Bullets[i].spawnX > 800 ||
                    Bullets[i].spawnY < 0 || Bullets[i].spawnY > 600)
                {
                    Bullets.RemoveAt(i);
                }
            }
        }

        public abstract void Move();

        public virtual void Attack()
        {
            // Her düşman tipi için 3 mermi ateş et
            double[] angles = { -15, 0, 15 }; // Açılar (derece cinsinden)
            foreach (var angle in angles)
            {
                double radians = angle * Math.PI / 180.0;
                double bulletSpeedX = -bullet_speed * Math.Cos(radians);
                double bulletSpeedY = bullet_speed * Math.Sin(radians);
                var bullet = new Bullet(spawnX, spawnY + Height / 2, bulletSpeedX, bulletSpeedY, 10, true);
                Bullets.Add(bullet);
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0) health = 0;
        }

        public bool IsOffScreen()
        {
            return spawnX < -50 || spawnX > 850 || spawnY < -50 || spawnY > 650;
        }
    }
}
