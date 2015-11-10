using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class OctyanEnemy : Enemy
    {
        public OctyanEnemy(ShootingGame s)
            : base(s)
        {
            Radius = 20;
            Position = new Position(rnd.Next(Radius, 640 - Radius), -Radius);
        }

        public override void Update()
        {
            ax += (rnd.NextDouble() - 0.5) * 2;
            Position.X += ax;
            if (Position.X < 0)
            {
                Position.X = 640;
            }
            if (Position.X > 640)
            {
                Position.X = 0;
            }

            ay += rnd.NextDouble() - 0.5;
            Position.Y += ay;
        }

        public override void Draw()
        {
            DX.DrawCircle((int)Position.X, (int)Position.Y, Radius, DX.GetColor(255, 20, 255));
        }

        private double ax = 0.0;
        private double ay = 5.0;
        private Random rnd = new Random();
    }
}
