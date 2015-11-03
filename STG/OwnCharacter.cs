using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    /// <summary>
    /// 自機を表す。
    /// </summary>
    public class OwnCharacter
    {
        /// <summary>
        /// 自機を初期化する。
        /// </summary>
        public OwnCharacter(Position pos, int radius, uint color)
        {
            Position = pos.Clone();
            Radius = radius;
            Color = color;
            speed = 5.0;
        }

        /// <summary>
        /// 1フレームぶんの処理を行う。
        /// </summary>
        /// <param name="keys">GetHitKeyStateAll で取得したキー入力情報</param>
        public void Update(byte[] keys)
        {
            // 移動(斜め移動が速くなるのは気にしないことにした)
            if (keys[DX.KEY_INPUT_UP] != 0)
            {
                Position.Y -= speed;
            }
            if (keys[DX.KEY_INPUT_LEFT] != 0)
            {
                Position.X -= speed;
            }
            if (keys[DX.KEY_INPUT_DOWN] != 0)
            {
                Position.Y += speed;
            }
            if (keys[DX.KEY_INPUT_RIGHT] != 0)
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
        }

        /// <summary>
        /// 自機を描画する。
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

        // 速度[px/frame]
        private double speed;
    }
}
