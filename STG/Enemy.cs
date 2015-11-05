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
        public Enemy(ShootingGame game)
        {
            Game = game;
        }

        /// <summary>
        /// 1フレームぶんの処理を行う。
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Enemy を描画する。
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// ゲーム本体
        /// </summary>
        public ShootingGame Game { get; protected set; }

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
