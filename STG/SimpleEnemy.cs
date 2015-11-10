using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class SimpleEnemy : Enemy
    {
        /// <summary>
        /// SimpleEnemy を初期化する。
        /// </summary>
        public SimpleEnemy(ShootingGame game)
            : base(game)
        {
            Radius = 20;
            Position = new Position(rnd.Next(Radius, 640 - Radius), -Radius);
            Color = DX.GetColor(0, 0, 255);
            speed = 3.0;
            angle = Math.PI / 2;
        }

        /// <summary>
        /// 1フレームぶんの処理を行う。
        /// </summary>
        /// <param name="bullets"></param>
        /// <param name="ownPos"></param>
        public override void Update()
        {
            // 移動
            Position.X += speed * Math.Cos(angle);
            Position.Y += speed * Math.Sin(angle);

            // 弾を撃つ
            ++frameCnt;
            if (frameCnt == 40)
            {
                frameCnt = 0;
                var bulletAngle = Math.Atan2(Game.OwnChar.Position.Y - Position.Y, Game.OwnChar.Position.X - Position.X);
                Game.EnemyBullets.Add(new Bullet(Position, 10, bulletAngle, 5.0, DX.GetColor(128, 255, 255)));
            }
        }

        /// <summary>
        /// 描画する。
        /// </summary>
        public override void Draw()
        {
            DX.DrawCircle((int)Position.X, (int)Position.Y, Radius, Color);
        }

        /// <summary>
        /// 色
        /// </summary>
        public uint Color { get; protected set; }

        private Random rnd = new Random();

        // 進行方向[rad]
        private double angle;

        // 速度[px/frame]
        private double speed;

        private int frameCnt = 0;
    }
}
