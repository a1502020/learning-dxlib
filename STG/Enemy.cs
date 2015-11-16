using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg
{
    public abstract class Enemy
    {
        public Enemy(ShootingGame game)
        {
            Game = game;
            Position = new Position();
            Dead = false;
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
        /// 敵が攻撃された時の処理。
        /// </summary>
        /// <param name="damage"></param>
        public virtual void OnDamaged(int damage)
        {
            if (damage > 0)
            {
                Dead = true;
            }
        }

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

        /// <summary>
        /// 死んだか否か
        /// </summary>
        public bool Dead { get; protected set; }
    }
}
