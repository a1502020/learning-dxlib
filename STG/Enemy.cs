using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public abstract class Enemy
    {
        /// <summary>
        /// 1フレームぶんの処理を行う。
        /// </summary>
        public abstract void Update(List<Bullet> bullets, Position ownPos);

        /// <summary>
        /// Enemy を描画する。
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// 位置
        /// </summary>
        public Position Position { get; protected set; }

        /// <summary>
        /// 半径
        /// </summary>
        public int Radius { get; protected set; }
    }
}
