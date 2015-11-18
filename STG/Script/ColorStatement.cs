using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public class ColorStatement : Statement
    {
        public ColorStatement(ShootingGame game, List<string> args)
        {
            this.game = game;
            if (args.Count < 4)
            {
                throw new FormatException("color with too few arguments.");
            }
            r = int.Parse(args[1]);
            g = int.Parse(args[2]);
            b = int.Parse(args[3]);
            frame = (args.Count >= 5) ? int.Parse(args[4]) : 1;
        }

        public override ScriptTask Run()
        {
            return new BackColorTask(game, r, g, b, frame);
        }

        private ShootingGame game;
        private int r, g, b;
        private int frame;
    }
}
