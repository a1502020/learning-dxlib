using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    class StraightEnemyFactory : EnemyFactory
    {
        public StraightEnemyFactory(ShootingGame game)
        {
            this.game = game;
        }

        public override Enemy Create()
        {
            return new StraightEnemy(game, Position, Angle, Speed, Interval);
        }

        /// <summary>
        /// 初期位置
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// 進行方向[rad]
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// 速度[px/frame]
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// 弾の発射間隔[frame]
        /// </summary>
        public int Interval { get; set; }

        private ShootingGame game;
    }
}
