using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class BoarEnemy : Enemy
    {
        public BoarEnemy(ShootingGame game, Position pos)
            : base(game)
        {
            this.Position = pos.Clone();
            Radius = 20;
            color = DX.GetColor(255, 128, 0);
            speed = 3.0;
            angle = Math.Atan2(game.OwnChar.Position.Y - Position.Y, game.OwnChar.Position.X - Position.X);
        }

        /// <summary>
        /// 1フレームぶんの処理を行う。
        /// </summary>
        public override void Update()
        {
            // 移動
            Position.X += speed * Math.Cos(angle);
            Position.Y += speed * Math.Sin(angle);

            // 画面外に出たら消える
            if (Position.X < -Radius || Position.Y < -Radius || Position.X > 640 + Radius || Position.Y > 480 + Radius)
            {
                Dead = appeared;
            }
            else
            {
                appeared = true;
            }
        }

        /// <summary>
        /// 描画する。
        /// </summary>
        public override void Draw()
        {
            DX.DrawCircle((int)Position.X, (int)Position.Y, Radius, color);
        }

        private double angle;
        private double speed;
        private uint color;
        private bool appeared = false;
    }
}
