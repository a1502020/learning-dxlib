using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public abstract class ScriptTask
    {
        public abstract void Update();
        public abstract bool Done { get; }
    }
}
