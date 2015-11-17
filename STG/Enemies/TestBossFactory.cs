using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    class TestBossFactory : BossFactory
    {
        public TestBossFactory(ShootingGame game)
        {
            this.game = game;
        }

        public override BossEnemy Create()
        {
            return new TestBoss(game);
        }

        private ShootingGame game;
    }
}
