using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class TurnSprinklerEnemyFactory : TurnEnemyFactory
    {
        public TurnSprinklerEnemyFactory(ShootingGame game)
            : base(game)
        {
            this.game = game;
        }

        public override Enemy Create()
        {
            return new TurnSprinklerEnemy(
                game, StartPos, StartTime, StopPos, StopTime, LeavePos, LeaveTime, Interval, BulletsCount,
                StartAngle, EndAngle, Direction);
        }

        public double StartAngle { get; set; }
        public double EndAngle { get; set; }
        public TurnDirection Direction { get; set; }

        private ShootingGame game;
    }
}
