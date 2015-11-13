using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    class SimpleEnemyFactory : EnemyFactory
    {
        public SimpleEnemyFactory(ShootingGame game)
        {
            this.game = game;
        }

        public override Enemy Create()
        {
            return new SimpleEnemy(game);
        }

        private ShootingGame game;
    }
}
