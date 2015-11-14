using Stg.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public class EnemyStatement : Statement
    {
        public EnemyStatement(EnemyFactory factory)
        {
            this.factory = factory;
        }

        public override void Run(ShootingGame game)
        {
            game.Enemies.Add(factory.Create());
        }

        private EnemyFactory factory;
    }
}
