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
                throw new FormatException("e without enemy name.");
            }
            var name = args[1];
            if (name == "straight")
            {
                if (args.Count < 2 + 5)
                {
                    throw new FormatException("e straight with too few arguments.");
                }
                factory = new StraightEnemyFactory(game)
                {
                    Position = new Position(double.Parse(args[2]), double.Parse(args[3])),
                    Angle = double.Parse(args[4]) * Math.PI / 180,
                    Speed = double.Parse(args[5]),
                    Interval = int.Parse(args[6]),
                };
            }
            else if (name == "straight_s")
            {
                if (args.Count < 2 + 5)
                {
                    throw new FormatException("e straight with too few arguments.");
                }
                factory = new StraightSEnemyFactory(game)
                {
                    Position = new Position(double.Parse(args[2]), double.Parse(args[3])),
                    Angle = double.Parse(args[4]) * Math.PI / 180,
                    Speed = double.Parse(args[5]),
                    Interval = int.Parse(args[6]),
                };
            }
            else if (name == "boar")
            {
                if (args.Count < 2 + 2)
                {
                    throw new FormatException("e boar with too few arguments.");
                }
                factory = new BoarEnemyFactory(game)
                {
                    Position = new Position(double.Parse(args[2]), double.Parse(args[3])),
                };
            }
            else if (name == "turn")
            {
                if (args.Count < 2 + 11)
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
                    Interval = int.Parse(args[11]),
                    BulletsCount = int.Parse(args[12]),
                };
            }
            else if (name == "turn_s")
            {
                if (args.Count < 2 + 11)
                {
                    throw new FormatException("e turn_s with too few arguments.");
                }
                factory = new TurnSEnemyFactory(game)
                {
                    StartPos = new Position(double.Parse(args[2]), double.Parse(args[3])),
                    StartTime = int.Parse(args[4]),
                    StopPos = new Position(double.Parse(args[5]), double.Parse(args[6])),
                    StopTime = int.Parse(args[7]),
                    LeavePos = new Position(double.Parse(args[8]), double.Parse(args[9])),
                    LeaveTime = int.Parse(args[10]),
                    Interval = int.Parse(args[11]),
                    BulletsCount = int.Parse(args[12]),
                };
            }
            else if (name == "turn_sprinkler")
            {
                if (args.Count < 2 + 14)
                {
                    throw new FormatException("e turn_sprinkler with too few arguments.");
                }
                TurnDirection dir;
                if (args[13] == "left")
                {
                    dir = TurnDirection.Left;
                }
                else if (args[13] == "right")
                {
                    dir = TurnDirection.Right;
                }
                else
                {
                    throw new FormatException("TurnDirection value should be left or right.");
                }
                factory = new TurnSprinklerEnemyFactory(game)
                {
                    StartPos = new Position(double.Parse(args[2]), double.Parse(args[3])),
                    StartTime = int.Parse(args[4]),
                    StopPos = new Position(double.Parse(args[5]), double.Parse(args[6])),
                    StopTime = int.Parse(args[7]),
                    LeavePos = new Position(double.Parse(args[8]), double.Parse(args[9])),
                    LeaveTime = int.Parse(args[10]),
                    Interval = int.Parse(args[11]),
                    BulletsCount = int.Parse(args[12]),
                    Direction = dir,
                    StartAngle = double.Parse(args[14]) * Math.PI / 180,
                    EndAngle = double.Parse(args[15]) * Math.PI / 180,
                };
            }
            else
            {
                throw new FormatException(string.Format("Unknown enemy name \"{0}\"", name));
            }
        }

        public override ScriptTask Run()
        {
            game.Enemies.Add(factory.Create());
            return null;
        }

        private ShootingGame game;
        private EnemyFactory factory;
    }
}
