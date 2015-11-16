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
            else if (name == "turn")
            {
                if (args.Count < 2 + 9)
                {
                    throw new FormatException("e turn with too few arguments.");
                }
                factory = new TurnEnemyFactory(game)
                {
                    StartPos = new Position(double.Parse(args[2]), double.Parse(args[3])),
                    StartTime = int.Parse(args[4]),
                    StopPos = new Position(double.Parse(args[5]), double.Parse(args[6])),
                    StopTime = int.Parse(args[7]),
                    LeavePos = new Position(double.Parse(args[8]), double.Parse(args[9])),
                    LeaveTime = int.Parse(args[10]),
                };
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
