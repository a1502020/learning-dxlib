using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class RandomWalkerEnemyFactory : EnemyFactory
    {
        public RandomWalkerEnemyFactory(ShootingGame game)
            : base()
        {
            this.game = game;
        }

        public override Enemy Create()
        {
            return new RandomWalkerEnemy(game,
                RangeX1, RangeY1, RangeX2, RangeY2, StartPos, EndPos,
                MoveFrame, StopFrame, MoveCount);
        }

        public int RangeX1 { get; set; }
        public int RangeY1 { get; set; }
        public int RangeX2 { get; set; }
        public int RangeY2 { get; set; }
        public Position StartPos { get; set; }
        public Position EndPos { get; set; }
        public int MoveFrame { get; set; }
        public int StopFrame { get; set; }
        public int MoveCount { get; set; }

        private ShootingGame game;
    }
}
