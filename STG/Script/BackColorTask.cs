using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public class BackColorTask : ScriptTask
    {
        public BackColorTask(ShootingGame game, int r, int g, int b, int frame)
        {
            this.game = game;
            this.r = r;
            this.g = g;
            this.b = b;
            this.frameLen = frame;
            pr = game.BackR;
            pg = game.BackG;
            pb = game.BackB;
        }

        public override void Update()
        {
            ++frameCnt;
            game.BackR = pr + frameCnt * (r - pr) / frameLen;
            game.BackG = pg + frameCnt * (g - pg) / frameLen;
            game.BackB = pb + frameCnt * (b - pb) / frameLen;
        }

        public override bool Done
        {
            get { return frameCnt >= frameLen; }
        }

        private ShootingGame game;
        private int r, g, b;
        private int pr, pg, pb;
        private int frameLen, frameCnt = 0;
    }
}
