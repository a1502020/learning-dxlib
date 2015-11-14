using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public class ScriptRunner
    {
        public ScriptRunner(ShootingGame game, Script script)
        {
            this.game = game;
            this.Script = script;
        }

        public void Step()
        {
            while (pc < Script.Statements.Count && waitTime <= 0)
            {
                var st = Script.Statements[pc];
                st.Run(game);
                waitTime = st.WaitTime;
                ++pc;
            }
            if (waitTime > 0)
            {
                --waitTime;
            }
        }

        public bool Finished { get { return pc >= Script.Statements.Count && waitTime <= 0; } }

        public Script Script { get; private set; }

        private ShootingGame game;

        private int pc = 0;
        private int waitTime = 0;
    }
}
