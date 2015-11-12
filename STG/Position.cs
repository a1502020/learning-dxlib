using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg
{
    /// <summary>
    /// 2次元平面上の位置を表す。
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Position を (x, y) で初期化する。
        /// </summary>
        /// <param name="x">X 座標</param>
        /// <param name="y">Y 座標</param>
        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// X 座標
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y 座標
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Position を (0, 0) で初期化する。
        /// </summary>
        public Position()
        {
            X = 0;
            Y = 0;
        }

        public Position Clone()
        {
            return new Position(X, Y);
        }
    }
}
