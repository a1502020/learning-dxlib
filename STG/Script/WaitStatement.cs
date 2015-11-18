using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public class WaitStatement : Statement
    {
        public WaitStatement(int time)
        {
            this.time = time;
        }

        public override ScriptTask Run()
        {
            return null;
        }

        public override int WaitTime { get { return time; } }

        private int time;
    }
}
