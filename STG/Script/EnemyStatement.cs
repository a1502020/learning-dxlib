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
        public EnemyStatement(ShootingGame game, List<string> args)
        {
            this.game = game;
            if (args.Count < 2)
            {
                throw new FormatException();
            }
            var name = args[1];
            if (name == "simple")
            {
                factory = new SimpleEnemyFactory(game);
            }
            else if (name == "boar")
            {
                factory = new BoarEnemyFactory(game);
            }
            else
            {
                throw new FormatException(string.Format("Unknown enemy name \"{0}\"", name));
            }
        }

        public override void Run()
        {
            game.Enemies.Add(factory.Create());
        }

        private ShootingGame game;
        private EnemyFactory factory;
    }
}
