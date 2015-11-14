using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public abstract class Statement
    {
        public abstract void Run(ShootingGame game);
        public virtual int WaitTime { get { return 0; } }
    }
}
