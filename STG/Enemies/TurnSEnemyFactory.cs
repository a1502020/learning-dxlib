using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class TurnSEnemyFactory : TurnEnemyFactory
    {
        public TurnSEnemyFactory(ShootingGame game)
            : base(game)
        {
            this.game = game;
        }

        public override Enemy Create()
        {
            return new TurnSEnemy(game, StartPos, StartTime, StopPos, StopTime, LeavePos, LeaveTime, Interval, BulletsCount);
        }

        private ShootingGame game;
    }
}
