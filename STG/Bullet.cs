using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg
{
    public class Bullet
    {
        /// <summary>
        /// Bullet を初期化する。
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="radius">半径</param>
        /// <param name="angle">進行方向[rad]</param>
        /// <param name="speed">速度[px/frame]</param>
        /// <param name="color">色</param>
        public Bullet(int power, Position position, int radius, double angle, double speed, uint color)
        {
            Power = power;
            Position = position.Clone();
            Radius = radius;
            Angle = angle;
            Speed = speed;
            Color = color;
        }

        /// <summary>
        /// 1フレームぶんの処理を行う。
        /// </summary>
        public void Update()
        {
            Position.X += Speed * Math.Cos(Angle);
            Position.Y += Speed * Math.Sin(Angle);
        }

        /// <summary>
        /// 弾を描画する。
        /// </summary>
        public void Draw()
        {
            DX.DrawCircle((int)Position.X, (int)Position.Y, Radius, Color);
        }

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// 半径
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// 進行方向[rad]
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// 速度[px/frame]
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// 色
        /// </summary>
        public uint Color { get; set; }

        public Bullet Clone()
        {
            return new Bullet(Power, Position, Radius, Angle, Speed, Color);
        }
    }
}
