using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
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
        public Bullet(Position position, int radius, double angle, double speed, uint color)
        {
            Position = position.Clone();
            Radius = radius;
            Angle = angle;
            Speed = speed;
            Color = color;
        }

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
            return new Bullet(Position, Radius, Angle, Speed, Color);
        }
    }
}
