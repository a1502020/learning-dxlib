using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg
{
    /// <summary>
    /// 自機を表す。
    /// </summary>
    public class OwnCharacter
    {
        /// <summary>
        /// 自機を初期化する。
        /// </summary>
        public OwnCharacter(ShootingGame game, Position pos)
        {
            this.game = game;
            Position = pos.Clone();
            Radius = 9;
            speed = 5.0;
            img = DX.LoadGraph("img/own.bmp");
            bulletFrame = 0;
        }

        /// <summary>
        /// 1フレームぶんの処理を行う。
        /// </summary>
        /// <param name="keys">GetHitKeyStateAll で取得したキー入力情報</param>
        public void Update(Key key)
        {
            // 移動(斜め移動が速くなるのは気にしないことにした)
            if (key.IsPressing(DX.KEY_INPUT_UP))
            {
                Position.Y -= speed;
            }
            if (key.IsPressing(DX.KEY_INPUT_LEFT))
            {
                Position.X -= speed;
            }
            if (key.IsPressing(DX.KEY_INPUT_DOWN))
            {
                Position.Y += speed;
            }
            if (key.IsPressing(DX.KEY_INPUT_RIGHT))
            {
                Position.X += speed;
            }

            // 画面外に行かないようにする
            if (Position.X < Radius)
            {
                Position.X = Radius;
            }
            if (Position.Y < Radius)
            {
                Position.Y = Radius;
            }
            if (Position.X > 640 - Radius)
            {
                Position.X = 640 - Radius;
            }
            if (Position.Y > 480 - Radius)
            {
                Position.Y = 480 - Radius;
            }

            // 弾を発射
            if (bulletFrame == 0 && key.IsPressing(DX.KEY_INPUT_SPACE))
            {
                game.OwnBullets.Add(new Bullet(Position, 5, 3 * Math.PI / 2, 10.0, DX.GetColor(255, 255, 0)));
                bulletFrame = 12;
            }
            if (bulletFrame > 0)
            {
                --bulletFrame;
            }
        }

        /// <summary>
        /// 自機を描画する。
        /// </summary>
        public void Draw()
        {
            DX.DrawGraph((int)(Position.X - 16), (int)(Position.Y - 22), img, DX.TRUE);
        }

        /// <summary>
        /// 位置
        /// </summary>
        public Position Position { get; private set; }

        /// <summary>
        /// 半径
        /// </summary>
        public int Radius { get; private set; }

        private ShootingGame game;

        // 画像
        private int img;

        // 速度[px/frame]
        private double speed;

        // 弾の発射間隔カウンタ
        private int bulletFrame;
    }
}
