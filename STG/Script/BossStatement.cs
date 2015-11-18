using Stg.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    class BossStatement : Statement
    {
        public BossStatement(ShootingGame game, List<string> args)
        {
            this.game = game;
            if (args.Count < 2)
            {
                throw new FormatException("b without boss name.");
            }
            var name = args[1];
            if (name == "test")
            {
                factory = new TestBossFactory(game);
            }
            else
            {
                throw new FormatException(string.Format("Unknown boss name \"{0}\"", name));
            }
        }

        public override ScriptTask Run()
        {
            game.Boss = factory.Create();
            return null;
        }

        private ShootingGame game;
        private BossFactory factory;
    }
}
