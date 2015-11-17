using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    class BoarEnemyFactory : EnemyFactory
    {
        public BoarEnemyFactory(ShootingGame game)
        {
            this.game = game;
        }

        public override Enemy Create()
        {
            return new BoarEnemy(game, Position);
        }

        /// <summary>
        /// 初期位置
        /// </summary>
        public Position Position { get; set; }

        private ShootingGame game;
    }
}
