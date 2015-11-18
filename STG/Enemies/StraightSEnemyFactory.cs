using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class StraightSEnemyFactory : StraightEnemyFactory
    {
        public StraightSEnemyFactory(ShootingGame game)
            : base(game)
        {
        }

        public override Enemy Create()
        {
            return new StraightSEnemy(game, Position, Angle, Speed, Interval);
        }
    }
}
