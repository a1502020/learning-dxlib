using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class Enemy
    {
        /// <summary>
        /// Enemy を初期化する。
        /// </summary>
        public Enemy()
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
        public void Update(List<Bullet> bullets)
        {
            // 移動
            Position.X += speed * Math.Cos(angle);
            Position.Y += speed * Math.Sin(angle);

            // 弾を撃つ
            ++frameCnt;
            if (frameCnt == 10)
            {
                frameCnt = 0;
                bullets.Add(new Bullet(
                    Position, 10, rnd.NextDouble() * 2 * Math.PI, 5.0, DX.GetColor(0, 255, 255)
                    ));
            }
        }

        /// <summary>
        /// Enemy を描画する。
        /// </summary>
        public void Draw()
        {
            DX.DrawCircle((int)Position.X, (int)Position.Y, Radius, Color);
        }

        /// <summary>
        /// 位置
        /// </summary>
        public Position Position { get; private set; }

        /// <summary>
        /// 半径
        /// </summary>
        public int Radius { get; private set; }

        /// <summary>
        /// 色
        /// </summary>
        public uint Color { get; private set; }

        private Random rnd = new Random();

        // 進行方向[rad]
        private double angle;

        // 速度[px/frame]
        private double speed;

        private int frameCnt = 0;
    }
}
